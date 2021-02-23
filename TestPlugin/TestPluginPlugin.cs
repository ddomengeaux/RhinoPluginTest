using System.Diagnostics;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using Rhino.DocObjects;
using Rhino.UI;

namespace TestPlugin
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class TestPluginPlugin : Rhino.PlugIns.PlugIn
    {
        ///<summary>Gets the only instance of the TestPluginPlugin plug-in.</summary>
        public static TestPluginPlugin Instance { get; private set; }

        public TestPluginPlugin()
        {
            Instance = this;
        }

        // You can override methods here to change the plug-in behavior on
        // loading and shut down, add options pages to the Rhino _Option command
        // and mantain plug-in wide options in a document.
        protected override void ObjectPropertiesPages(ObjectPropertiesPageCollection collection)
        {
            collection.Add(new TestPage());
        }
    }

    public class TestPage : ObjectPropertiesPage
    {
        private TestControl _control;

        public override string EnglishPageTitle => "testtesttest";
        public override object PageControl => _control ??= new TestControl();

        public override void UpdatePage(ObjectPropertiesPageEventArgs e)
        {
            _control.Objects = e.Objects;
        }
    }

    public class TestControl : Panel
    {
        public TestControl()
        {
            var hello_button = new Button { Text = "Hello..." };
            hello_button.Click += (sender, e) =>
            {
                foreach (var obj in Objects)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        obj.Attributes.SetUserString("TEST", "TEST");

                        if (obj.CommitChanges())
                            Debug.WriteLine($"SUCCESS");
                        else
                            Debug.WriteLine($"FAILED");
                    }
                }
            };

            var layout = new DynamicLayout { DefaultSpacing = new Size(5, 5), Padding = new Padding(10) };
            layout.AddSeparateRow(hello_button, null);
            layout.Add(null);
            Content = layout;
        }

        public RhinoObject[] Objects { get; set; }
    }
}
