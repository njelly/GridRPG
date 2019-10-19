////////////////////////////////////////////////////////////////////////////////
//
//  UIStyleLibrary (c) 2019 Tofunaut
//
//  Created by Nathaniel Ellingson for GridRPG on 10/17/2019
//
////////////////////////////////////////////////////////////////////////////////

using System;
using UnityEngine;
using TMPro;
using Tofunaut.SharpUnity.UI;
using Tofunaut.SharpUnity.UI.Behaviour;
using Tofunaut.Animation;

namespace Tofunaut.GridRPG.UI
{
    public class UIStartMenuButton : SharpUIImage
    {
        private const float AnimTime = 0.2f;
        private const float PointerEnterScale = 1.05f;
        private const float PointerDownScale = 0.95f;
        private static readonly Color NormalColor = Color.white;
        private static readonly Color PointerEnterColor = new Color32(0x66, 0xff, 0x66, 0xff);
        private static readonly Color PointerDownColor = new Color32(0x33, 0xcc, 0x33, 0xff);

        private readonly Action _onClick;
        private TofuAnimation _pointerEnterAnimation;
        private TofuAnimation _pointerDownAnimation;

        public UIStartMenuButton(string name, string caption, Action onClick) : base(name, null)
        {
            _onClick = onClick;

            Color = NormalColor;

            SharpUITextMeshPro label = UIStyleLibrary.Text.Default(string.Format("{0}_Label", name), caption);
            label.TextAlignment = TextAlignmentOptions.Center;
            label.SetFillSize();
            label.Color = Color.black;
            AddChild(label);

            SubscribeToEvent(EEventType.PointerEnter, OnPointerEnter);
            SubscribeToEvent(EEventType.PointerDown, OnPointerDown);
            SubscribeToEvent(EEventType.PointerExit, OnPointerExit);
            SubscribeToEvent(EEventType.PointerClick, OnPointerClick);
        }

        public override void Destroy()
        {
            base.Destroy();

            _pointerEnterAnimation?.Stop();
            _pointerDownAnimation?.Stop();
        }

        private void OnPointerEnter(object sender, EventSystemEventArgs e)
        {
            _pointerEnterAnimation = BuildPointerEnterAnimation().Play();
        }

        private void OnPointerDown(object sender, EventSystemEventArgs e)
        {
            _pointerEnterAnimation?.Stop();
            _pointerDownAnimation = BuildPointerDownAnimation().Play();
        }

        private void OnPointerExit(object sender, EventSystemEventArgs e)
        {
            _pointerEnterAnimation?.Stop();
            _pointerDownAnimation?.Stop();

            Color = NormalColor;
            RectTransform.localScale = Vector3.one;
        }

        private void OnPointerClick(object sender, EventSystemEventArgs e)
        {
            _onClick?.Invoke();

            if (!IsBuilt)
            {
                return;
            }

            _pointerDownAnimation?.Stop();
            _pointerEnterAnimation = BuildPointerEnterAnimation().Play();
        }

        private TofuAnimation BuildPointerEnterAnimation()
        {
            Color startColor = Color;
            Vector3 startScale = RectTransform.localScale;

            return new TofuAnimation()
                .Value01(AnimTime, EEaseType.EaseOutExpo, (float newValue) =>
                {
                    Color = Color.LerpUnclamped(startColor, PointerEnterColor, newValue);
                    RectTransform.localScale = Vector3.LerpUnclamped(startScale, Vector3.one * PointerEnterScale, newValue);
                });
        }

        private TofuAnimation BuildPointerDownAnimation()
        {
            Vector3 startScale = RectTransform.localScale;
            Color startColor = Color;

            return new TofuAnimation()
                .Value01(AnimTime, EEaseType.EaseOutExpo, (float newValue) =>
                {
                    Color = Color.LerpUnclamped(startColor, PointerDownColor, newValue);
                    RectTransform.localScale = Vector3.LerpUnclamped(startScale, Vector3.one * PointerDownScale, newValue);
                });
        }
    }
}