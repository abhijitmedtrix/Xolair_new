using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTools
{
	public interface IUpdateCatcher
	{
		void ReceiveUpdate();
	}

	public interface IFixedUpdateCatcher
	{
		void ReceiveFixedUpdate();
	}

	public class UpdateManager : MonoBehaviour
	{
		private static readonly List<IUpdateCatcher> _updateCatchers = new List<IUpdateCatcher>();
		private static readonly List<IFixedUpdateCatcher> _fixedUpdateCatchers = new List<IFixedUpdateCatcher>();
		private static readonly List<Coroutine> _coroutines = new List<Coroutine>();

		private static readonly List<IUpdateCatcher> _updateCatchersToDelete = new List<IUpdateCatcher>();
		private static readonly List<IFixedUpdateCatcher> _fixedUpdateCatchersToDelete = new List<IFixedUpdateCatcher>();

		private static bool _updateInProgress;
		private static bool _fixedUpdateInProgress;

		private static UpdateManager _instance;

		public static void AddUpdateCatcher(IUpdateCatcher updateCatcher)
		{
			if (_updateCatchers.Contains(updateCatcher))
				return;

			_updateCatchers.Add(updateCatcher);
		}

		public static void AddFixedUpdateCatcher(IFixedUpdateCatcher fixedUpdateCatcher)
		{
			if (_fixedUpdateCatchers.Contains(fixedUpdateCatcher))
				return;

			_fixedUpdateCatchers.Add(fixedUpdateCatcher);
		}

		public static void RemoveUpdateCatcher(IUpdateCatcher updateCatcher)
		{
			if (_updateInProgress)
			{
				_updateCatchersToDelete.Add(updateCatcher);
				return;
			}

			_updateCatchers.Remove(updateCatcher);
		}

		public static void RemoveFixedUpdateCatcher(IFixedUpdateCatcher fixedUpdateCatcher)
		{
			if (_fixedUpdateInProgress)
			{
				_fixedUpdateCatchersToDelete.Add(fixedUpdateCatcher);
				return;
			}

			_fixedUpdateCatchers.Remove(fixedUpdateCatcher);
		}

		//--------------------------------------------------------------------------

		public static Coroutine PerformCoroutine(IEnumerator enumerator)
		{
			Coroutine coroutine = _instance.StartCoroutine(enumerator);
			_coroutines.Add(coroutine);
			return coroutine;
		}

		public static void CancelCoroutine(Coroutine coroutine)
		{
			_instance.StopCoroutine(coroutine);
			_coroutines.Remove(coroutine);
		}

		//--------------------------------------------------------------------------

		private void Awake()
		{
			if (_instance != null)
			{
				ClearAll();
				return;
			}

			_instance = this;
		}

		private void OnDestroy()
		{
			ClearAll();
		}

		private void Update()
		{
			CheckUpdateCatchers();
			_updateInProgress = true;

			for (int i = 0; i < _updateCatchers.Count; ++i)
				_updateCatchers[i].ReceiveUpdate();

			_updateInProgress = false;
		}

		private void FixedUpdate()
		{
			CheckFixedUpdateCatchers();
			_fixedUpdateInProgress = true;

			for (int i = 0; i < _fixedUpdateCatchers.Count; ++i)
				_fixedUpdateCatchers[i].ReceiveFixedUpdate();

			_fixedUpdateInProgress = false;
		}

		private void ClearAll()
		{
			foreach (var coroutine in _coroutines)
			{
				if (coroutine != null) StopCoroutine(coroutine);
			}

			_coroutines.Clear();
			_updateCatchers.Clear();
			_fixedUpdateCatchers.Clear();
			_updateCatchersToDelete.Clear();
			_fixedUpdateCatchersToDelete.Clear();
		}

		private void CheckUpdateCatchers()
		{
			if (_updateCatchersToDelete.Count > 0)
			{
				for (int i = 0; i < _updateCatchersToDelete.Count; ++i)
				{
					_updateCatchers.Remove(_updateCatchersToDelete[i]);
				}

				_updateCatchersToDelete.Clear();
			}
		}

		private void CheckFixedUpdateCatchers()
		{
			if (_fixedUpdateCatchersToDelete.Count > 0)
			{
				for (int i = 0; i < _fixedUpdateCatchersToDelete.Count; ++i)
				{
					_fixedUpdateCatchers.Remove(_fixedUpdateCatchersToDelete[i]);
				}

				_fixedUpdateCatchersToDelete.Clear();
			}
		}
	}
}