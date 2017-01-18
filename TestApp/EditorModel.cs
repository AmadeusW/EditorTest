using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class EditorModel
    {
        internal string Committed { get; private set; }
        internal string Transient { get; private set; }
        private Queue<EditorAction> Queued { get; set; }

        public EditorModel()
        {
            Committed = String.Empty;
            Transient = String.Empty;
            Queued = new Queue<EditorAction>();
        }

        public void Enqueue(EditorAction action)
        {
            Queued.Enqueue(action);
            if (action.Command == EditorCommand.TypeCharacter)
            {
                Transient += action.Param;
            }
        }

        internal IEnumerable<string> GetJobs()
        {
            return Queued.Select(n => $"{n.Command.ToString()}({n.Param})");
        }
    }
}
