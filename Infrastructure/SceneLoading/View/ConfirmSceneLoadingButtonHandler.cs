using System;
using System.Collections.Generic;
using EventBus.Public;
using InternalEvents.Public;
using ModuleRegistration.Public;
using SceneLoading.Data;
using UnityEngine.UI;

namespace SceneLoading.View
{
	internal sealed class ConfirmSceneLoadingButtonHandler : IEventHandler<MonoReferenceLoaded>, IEventHandler<SceneLoaded>
	{
		[Inject] private readonly IEventBus _eventBus;
		
		private readonly List<WeakReference<Button>> _buttons = new List<WeakReference<Button>>();

		public void Handle(MonoReferenceLoaded eventData)
		{
			if (eventData.Type != typeof(ConfirmSceneLoadingButtonData)) return;
			
			var reference = (ConfirmSceneLoadingButtonData)eventData.Reference;
			var button = reference.Button;
			
			_buttons.Add(new WeakReference<Button>(button));
			button.interactable = false;
		}

		public void Handle(SceneLoaded eventData)
		{
			var confirm = eventData.Confirm;

			foreach (var reference in _buttons)
			{
				if (!reference.TryGetTarget(out var button)) continue;
				
				button.onClick.AddListener(() => confirm());
				button.interactable = true;
			}
		}
	}
}