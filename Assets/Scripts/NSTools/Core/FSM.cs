namespace NSTools
{

    public interface ISignalHandler<T>
    {
        public AState Signal(T signal);
    }

    public struct ASignal
    {
        public string name;
        public object arg;
    
        public ASignal(string name, object arg = null)
        {
            this.name = name;
            this.arg = arg;
        }
    }
    public class AState
    {
        public virtual void Enter() { }

        public virtual void Exit() { }

        
    }
    
    
    public class FSM
    {
        private AState currentState;

        /// <summary>
        /// Change state
        /// </summary>
        /// <param name="newState">State to go</param>
        public void Go(AState newState)
        {
            if (newState == null) return;
            Log.Info($"FSM:Go({newState})");
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }

        /// <summary>
        /// Send signal to current state
        /// </summary>
        /// <param name="signal">Signal value</param>
        /// <typeparam name="T">Signal type</typeparam>
        public void Signal<T>(T signal)
        {
            Log.Debug($"FSM.Signal - {signal}");
            if (currentState is ISignalHandler<T> handler)
            {
                var nextState = handler.Signal(signal);
                Go(nextState);
                return;
            }
            Log.Info($"State {currentState} has no handler for {signal}");
        }
    }
}