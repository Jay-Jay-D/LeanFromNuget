using QuantConnect.Lean.Engine;
using QuantConnect.Util;

namespace LeanFromNuget
{
    class Program
    {
        static void Main(string[] args)
        {
            var liveMode = false;
            LeanEngineSystemHandlers leanEngineSystemHandlers;
            leanEngineSystemHandlers = LeanEngineSystemHandlers.FromConfiguration(Composer.Instance);
            leanEngineSystemHandlers.Initialize();

            var job = leanEngineSystemHandlers.JobQueue.NextJob(out var assemblyPath);

            var leanEngineAlgorithmHandlers = LeanEngineAlgorithmHandlers.FromConfiguration(Composer.Instance);

            var algorithmManager = new AlgorithmManager(liveMode);

            leanEngineSystemHandlers.LeanManager.Initialize(leanEngineSystemHandlers, leanEngineAlgorithmHandlers, job, algorithmManager);
            var engine = new Engine(leanEngineSystemHandlers, leanEngineAlgorithmHandlers, liveMode);
            engine.Run(job, algorithmManager, assemblyPath);
        }
    }
}
