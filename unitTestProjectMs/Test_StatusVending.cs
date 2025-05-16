namespace unitTestProjectMs;
using UTS_Evi;
using StatusState;


[TestClass]
public class Test_StatusVending
{

    StatusState.statusVending Sv = new StatusState.statusVending();
    
    [TestMethod]
    public void Test_Set_Idle()
    {
        Assert.AreEqual("Idle", Sv.set_Status_Idle());
        Assert.AreEqual("Idle", Sv.Get_Current_Status());

    }

    [TestMethod]
    public void Test_Get_Current_Status()
    {
        Assert.AreEqual("Idle", Sv.Get_Current_Status());
    }

    [TestMethod]
    public void Test_Set_Order()
    {
        Assert.AreEqual("Order", Sv.set_Status_Order());
        Assert.AreEqual("Order", Sv.Get_Current_Status());


    }



    [TestMethod]
    public void Test_Set_Payment()
    {
        Assert.AreEqual("Payment", Sv.set_Status_Payment());
        Assert.AreEqual("Payment", Sv.Get_Current_Status());

    }

    [TestMethod]
    public void Test_Set_Done()
    {
        Assert.AreEqual("Done", Sv.set_Status_Done());
        Assert.AreEqual("Done", Sv.Get_Current_Status());
    }
}
