namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        private string GetPerTestAttachmentsDirectory()
        {
            var path = TestContext.CurrentContext.WorkDirectory;
            path = System.IO.Path.Combine(path, TestContext.CurrentContext.Test.ID.ToString());
            System.IO.Directory.CreateDirectory(path);
            return path;
        }
        

        [Test]
        public void Test1()
        {
            var attachmentDir = GetPerTestAttachmentsDirectory();

            var file1 = System.IO.Path.Combine(attachmentDir, "I want to open this Report in a web browser, NOT in a VS tab.html");
            System.IO.File.WriteAllText(file1, "<html><body><div>Hello World</div></body></html>");
            TestContext.AddTestAttachment(file1);

            var file2 = System.IO.Path.Combine(attachmentDir, "I want to open this 3D model in Windows's AR viewer, NOT in a VS tab.glb");
            var bytes = System.IO.File.ReadAllBytes("box.glb");
            System.IO.File.WriteAllBytes(file2, bytes);
            TestContext.AddTestAttachment(file2);
        }
    }
}