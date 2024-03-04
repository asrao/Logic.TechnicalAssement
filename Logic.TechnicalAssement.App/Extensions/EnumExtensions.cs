using Logic.TechnicalAssement.App.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace InvestmentManagement.Shared.Extensions
{
	public static class EnumExtensions
	{
		public static IList<SelectLeaveType> EnumDetails()
		{
			var enumData = (from LeaveTypeEnum x in Enum.GetValues(typeof(LeaveTypeEnum))
							select new SelectLeaveType

							{
								DisplayName = GetDisplayName(x),

								Name = x.ToString()
							}).ToList();

			return enumData.ToList<SelectLeaveType>();
		}

        public static string GetDisplayName(this Enum value)
		{
			return value.GetType()?
							.GetMember(value.ToString())?
							.First()?
							.GetCustomAttribute<DisplayAttribute>()?
							.GetName();
		}
	}

	public static class Utilities
	{
		static Random objRand = new Random();

		/// <summary>
		/// Generates a Unique long int value
		/// </summary>
		/// <returns>long data</returns>
		public static long GeneratUniqueId()
		{
			
			return objRand.NextInt64(DateTime.Now.Ticks, DateTime.Now.Ticks+2);	
		}
	}

	public class SelectLeaveType
    {
		public string Name { get; set; }
		public string DisplayName { get; set; }
	}
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }

    
}
