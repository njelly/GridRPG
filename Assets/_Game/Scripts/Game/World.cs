using System;
using System.Collections.Generic;
using System.IO;
using Tofunaut.Animation;
using Tofunaut.Core;
using Tofunaut.SharpUnity;
using Tofunaut.UnityUtils;
using UnityEngine;

namespace Tofunaut.GridRPG.Game
{
    public class World : SharpGameObject
    {
        public const int CachedRegionDistance = 3;
        public const int SimulateRegionDistance = 2;
        public const int RenderRegionDistance = 1;

        public static string WorldStateSavePath { get { return Path.Combine(Application.persistentDataPath, "WorldState.grpgsave"); } }

        public uint ActorIdCounter => _state.actorIdCounter;

        private readonly WorldState _state;
        private readonly Player _player;

        private Dictionary<IntVector2, Region> _coordToRegion;

        protected World(WorldState state) : base("World")
        {
            _state = state;
            _player = new Player(this, _state.playerState);

            _coordToRegion = new Dictionary<IntVector2, Region>();
        }

        public void Tick()
        {
            Region playerRegion = _player.CurrentActor.CurrentTile.region;
            for (int x = -SimulateRegionDistance; x >= SimulateRegionDistance; x++)
            {
                for (int y = -SimulateRegionDistance; y >= SimulateRegionDistance; y++)
                {
                    IntVector2 coord = new IntVector2(x, y) + playerRegion.coord;
                    _coordToRegion[coord].Tick();
                }
            }
        }

        public Region GetRegion(IntVector2 coord)
        {
            if (_coordToRegion.TryGetValue(coord, out Region toReturn))
            {
                return toReturn;
            }
            else
            {
                Debug.Log($"Region not loaded for {coord}");
            }

            return null;
        }

        public void SetCenterRegion(IntVector2 coord, Action onComplete)
        {
            TofuAnimation currentLoadAnim = null;
            int numRegionsLoaded = 0;
            for (int x = -SimulateRegionDistance; x <= SimulateRegionDistance; x++)
            {
                for (int y = -SimulateRegionDistance; y <= SimulateRegionDistance; y++)
                {
                    IntVector2 regionCoord = coord + new IntVector2(x, y);
                    new TofuAnimation()
                        .WaitUntil(() =>
                        {
                            // by waiting for the current loadAnim to be null, we ensure that we only generate/load one region at a time (since it will be threaded)
                            return currentLoadAnim == null;
                        })
                        .Then()
                        .Execute(() =>
                        {
                            RegionState regionState = null;
                            currentLoadAnim = new TofuAnimation()
                                .Execute(() =>
                                {
                                    LoadRegionState(regionCoord, (RegionState payload) =>
                                    {
                                        regionState = payload;
                                    });
                                })
                                .WaitUntil(() =>
                                {
                                    return regionState != null;
                                })
                                .Then()
                                .Execute(() =>
                                {
                                    _state.AddRegionState(regionCoord, regionState);
                                    numRegionsLoaded++;
                                    currentLoadAnim = null;
                                })
                                .Play();
                        })
                        .Play();
                }
            }

            new TofuAnimation()
                .WaitUntil(() =>
                {
                    return numRegionsLoaded == ((SimulateRegionDistance * 2) + 1) * ((SimulateRegionDistance * 2 + 1));
                })
                .Then()
                .Execute(() =>
                {
                    onComplete();
                })
                .Play();
        }

        public void RenderRegions(IntVector2 centerCoord)
        {
            if (!IsBuilt)
            {
                return;
            }

            for (int x = -SimulateRegionDistance; x <= SimulateRegionDistance; x++)
            {
                for (int y = -SimulateRegionDistance; y <= SimulateRegionDistance; y++)
                {
                    IntVector2 coord = new IntVector2(x, y) + centerCoord;

                    // make sure it exists in the first place
                    if (!_coordToRegion.TryGetValue(coord, out Region region))
                    {
                        if (_state.TryGetRegionState(coord, out RegionState regionState))
                        {
                            region = new Region(this, regionState);
                            _coordToRegion.Add(coord, region);
                        }
                        else
                        {
                            Debug.LogError($"the RegionState at {coord} must exist before attempting to render it!");
                            continue;
                        }
                    }

                    if (x >= -RenderRegionDistance && x <= RenderRegionDistance && y >= -RenderRegionDistance && y <= RenderRegionDistance)
                    {
                        if (region.Parent != this)
                        {
                            this.AddChild(region);
                        }
                        region.LocalPosition = (new IntVector2(x, y) * RegionState.Size).ToUnityVector3_XY();
                    }
                    else if (region.IsBuilt)
                    {
                        // do not render regions outside the RenderRegionDistance
                        region.Destroy();
                    }
                }
            }

            // now remove any regions outside the CachedRedgionDistance
            List<IntVector2> toRemove = new List<IntVector2>();
            foreach (IntVector2 key in _coordToRegion.Keys)
            {
                IntVector2 toCenter = centerCoord - key;
                if (Mathf.Abs(toCenter.x) > CachedRegionDistance || Mathf.Abs(toCenter.y) > CachedRegionDistance)
                {
                    toRemove.Add(key);
                }
            }
            foreach (IntVector2 coord in toRemove)
            {
                _coordToRegion.Remove(coord);
            }
        }

        private void LoadRegionState(IntVector2 regionCoord, Action<RegionState> onComplete)
        {
            if (_state.TryGetRegionState(regionCoord, out RegionState regionState))
            {
                onComplete(regionState);
            }
            else
            {
                RegionStateGen.GenerateOnThread(regionCoord, onComplete);
            }
        }

        public void Save(Action onComplete = null)
        {
            string path = WorldStateSavePath;
            File.WriteAllBytes(path, GZipUtil.Compress(_state.ToString()));
            Debug.Log($"saved WorldState data to {path}");
            onComplete?.Invoke();
        }

        public static void Load(Action<World> onComplete)
        {
            string path = WorldStateSavePath;
            World toReturn = null;

            if (File.Exists(path))
            {
                string data = GZipUtil.Decompress(File.ReadAllBytes(path)).Trim();
                try
                {
                    WorldState worldState = Newtonsoft.Json.JsonConvert.DeserializeObject<WorldState>(data);
                    Debug.Log($"loaded saved WorldState, seed: {worldState.seed}");
                    toReturn = new World(worldState);
                }
                catch
                {
                    Debug.LogError("could not deserialize saved world state data");
                    onComplete?.Invoke(null);
                }
            }
            else
            {
                Debug.LogFormat($"Could not find file at {path}, so it will be created");

                // check to make sure the directory exists before trying to write a file to it
                if (!Directory.Exists(Application.streamingAssetsPath))
                {
                    Directory.CreateDirectory(Application.streamingAssetsPath);
                }

                WorldState worldState = new WorldState(UnityEngine.Random.Range(int.MinValue, int.MaxValue));
                toReturn = new World(worldState);
            }

            toReturn.Save();
            onComplete(toReturn);
        }

        public uint GetActorId()
        {
            return ++_state.actorIdCounter;
        }
    }
}