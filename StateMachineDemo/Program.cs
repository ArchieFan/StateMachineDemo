using Stateless;
using Stateless.Graph;

namespace StateMachine
{


    class Program
    {
        enum Input
        {
            Coin,
            Push
        }
        enum State
        {
            Locked,
            Unlocked
        }
        static void Main()
        {
            var stateMachine = new StateMachine<State, Input>(State.Locked);
            stateMachine.Configure(State.Locked)
                .Permit(Input.Coin, State.Unlocked)
                .PermitReentry(Input.Push);
            stateMachine.Configure(State.Unlocked)
              .Permit(Input.Push, State.Locked)
              .PermitReentry(Input.Coin);
            stateMachine.Fire(Input.Coin);
            Console.WriteLine(stateMachine.State);

            var graph = UmlDotGraph.Format(stateMachine.GetInfo());
            Console.WriteLine(graph);

            Console.ReadLine();
        }
    }
}

