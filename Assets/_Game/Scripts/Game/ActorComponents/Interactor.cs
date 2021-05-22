namespace Tofunaut.GridRPG.Game.ActorComponents
{
    public class Interactor : ActorComponent
    {
        private bool _wasInteracting;
        
        private void Update()
        {
            var isInteracting = Actor.Input.Interacting;
            if (!_wasInteracting && isInteracting)
            {
                BeginInteracting();
            }
            else if(_wasInteracting && !isInteracting)
            {
                EndInteracting();
            }
        }

        private void BeginInteracting()
        {
            _wasInteracting = true;
        }

        private void EndInteracting()
        {
            _wasInteracting = false;
        }
    }
}