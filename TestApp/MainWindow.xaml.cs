using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EditorModel Model { get; }

        public MainWindow()
        {
            InitializeComponent();
            Model = new EditorModel();
        }

        private void InputBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.A && e.Key <= Key.Z)
            {
                Model.Enqueue(new EditorAction()
                {
                    Command = EditorCommand.TypeCharacter,
                    Param = e.Key.ToString()
                });
            }
            else if (e.Key == Key.F1)
            {
                Model.Enqueue(new EditorAction()
                {
                    Command = EditorCommand.InsertSnippet,
                    Param = "for"
                });
            }
            e.Handled = true;
            Render();
        }

        private void RenderCore()
        {
            var transient = new Run(Model.Transient);
            transient.FontStyle = FontStyles.Italic;

            InputBox.Text = String.Empty;
            InputBox.Inlines.Add(Model.Committed);
            InputBox.Inlines.Add(transient);

            JobBox.Text = String.Empty;
            foreach (var element in Model.GetJobs())
            {
                JobBox.Text += element;
            }
        }

        internal void Render()
        {
            InputBox.Dispatcher.Invoke(RenderCore);
        }
    }
}
