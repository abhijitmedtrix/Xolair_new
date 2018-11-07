using System;
 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class AppManager : MonoSingleton<AppManager>
 {
     private DateTime _currentDate;
     
     public enum Mode
     {
         SSA,
         CSU
     }
 
     public Mode CurrentMode;

     public Action<Mode> OnModeChange;
 
     public void SetMode(Mode mode)
     {
         CurrentMode = mode;
         OnModeChange?.Invoke(CurrentMode);
     }

     #region Test

     // test method to add day to current date (avoiding using System DateTime.now)
     public void AddDay()
     {
         _currentDate = _currentDate.AddDays(1);
     }
     
     public DateTime GetNextDay()
     {
         return _currentDate.AddDays(1);
     }
     
     // test method to remove day to current date
     public void RemoveDay()
     {
         _currentDate = _currentDate.AddDays(-1);
     }
     
     public DateTime GetPreviousDay()
     {
         return _currentDate.AddDays(-1);
     }

     #endregion
 }