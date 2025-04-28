using System;
using System.Collections.Generic;
using EventBus.Public;
using InternalEvents.Public;
using ModuleRegistration.Public;
using SceneLoading.Data;
using UnityEngine.UI;

namespace SceneLoading.View
{
	internal sealed class ProgressSliderHandler : IEventHandler<MonoReferenceLoaded>, IEventHandler<SceneLoadingProgressChanged>
	{
		[Inject] private readonly IEventBus _eventBus;
		
		private readonly List<WeakReference<Slider>> _sliders = new List<WeakReference<Slider>>();
		
		public void Handle(MonoReferenceLoaded eventData)
		{
			if (eventData.Type != typeof(ProgressSliderReferenceData)) return;
			
			var reference = (ProgressSliderReferenceData)eventData.Reference;

			var slider = reference.Slider;
			_sliders.Add(new WeakReference<Slider>(slider));
			slider.interactable = false;
			slider.maxValue = 0f;
			slider.maxValue = 1f;
		}

		public void Handle(SceneLoadingProgressChanged eventData)
		{
			var value = eventData.Progress;

			foreach (var reference in _sliders)
			{
				if (!reference.TryGetTarget(out var slider)) continue;
				slider.value = value;
			}
		}
	}
}