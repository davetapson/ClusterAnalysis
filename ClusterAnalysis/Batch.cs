using Serilog;

namespace ClusterAnalysis
{
    internal class Batch
    {
        internal void Run()
        {
            Log.Information("In run...");
            Log.Information("About to do stuff...");

            // stuff

            // so, you need to find out how to import the data, and then hold it in a fashion that lets you investigate it.     // done
            // first prize is to save the files on your disk somewhere and then read them from there => file handling           // done
            // into a db => db handling                 // done
            // and then save the results into db!       // done

            // once you have the data, you are looking for:
            // result 1: ring1 with >= 4 positives (i.e. a positive value surrounded by >= 4 positive values)
            // result 2: ring1 with >= 7 positives
            // result 3: result 2 plus ring2 with >= 13 positives

            // ideally you'd be looking to save the results of your investigation somehow, so that you can go back and see 
            // what the results were as opposed to having to run the system again.

            // I've just done it, let's see what you come up with :)
            // Maybe I will learn something.

            // I use BareTail to monitor logs - you run BareTail, point it at the log file and then it updates as the log changes.
            // It doesn't lock the log file like notepad would do.  Notepad++ doesn't lock the file (which is why it's nice) but 
            // it doesn't automatically update when the log changes.
            // https://www.baremetalsoft.com/baretail/

            // there are some data files in the DataFiles folder


            // comment to create a change to show Mike how to push code...
        }
    }
}