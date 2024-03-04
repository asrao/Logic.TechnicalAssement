using Moq;
using Logic.TechnicalAssement.App.Models;
using Logic.TechnicalAssement.App.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InvestmentManagement.Shared.Extensions;
namespace LeaveAppTest
{
    public class LeaveAppTests
    {
        [Fact]
        public void Test_For_Getting_DispalyName_For_Leave_Type_Enum()
        {
            string strExpected_DisplayName = "Compassionate Leave";
            string strActual_DiplayName_Got = LeaveTypeEnum.CompassionateLeave.GetDisplayName();
            Assert.Equal(strExpected_DisplayName, strActual_DiplayName_Got);
             
        }
        [Fact]
        public void Test_For_Unique_Id_For_LeaveApplication_Id()
        {
            long lExpected_id = DateTime.Now.Ticks + 100;
            long lActualid_generated = Utilities.GeneratUniqueId();
            Assert.NotEqual(lExpected_id, lActualid_generated);
        }
        
        
       
    }
}