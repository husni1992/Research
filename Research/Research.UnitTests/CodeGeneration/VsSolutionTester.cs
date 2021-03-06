﻿
namespace Research.UnitTests.CodeGeneration
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Research.Core.CodeGeneration;

    [TestClass]
    public class VsSolutionTester
    {
        //AddCommonAssemblyInfo
        [TestMethod]
        public void AddCommonAssemblyInfo()
        {
            string input = @"<ItemGroup>
    <Compile Include=""Properties\\AssemblyInfo.cs"" />
  </ItemGroup>";

            string expected = @"<ItemGroup>
    <Compile Include=""..\\SharedSource\\CommonAssemblyInfo.cs"">
      <Link>CommonAssemblyInfo.cs</Link>
    </Compile>
    <Compile Include=""Properties\\AssemblyInfo.cs"" />
  </ItemGroup>";

            var solution = new VsSolution();
            string actual = solution.AddCommonAssemblyInfo(input);

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void RemoveCommonAssemblyInfo()
        {
            string input = @"[assembly: AssemblyTitle(""Research.Core"")]
[assembly: AssemblyDescription("""")]
[assembly: AssemblyConfiguration("""")]
[assembly: AssemblyCompany("""")]
[assembly: AssemblyProduct(""Research.Core"")]
[assembly: AssemblyVersion(""1.0.0.0"")]
[assembly: AssemblyFileVersion(""1.0.0.0"")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion(""1.0.*"")]";

            string expected = @"[assembly: AssemblyTitle(""Research.Core"")]
[assembly: AssemblyDescription("""")]
[assembly: AssemblyConfiguration("""")]
[assembly: AssemblyProduct(""Research.Core"")]";

            var solution = new VsSolution();
            string actual = solution.RemoveCommonAssemblyInfo(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
