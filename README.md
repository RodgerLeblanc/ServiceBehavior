# ServiceBehavior
Behavior as a service in a WPF app.

Code highly inspired by [DevExpress documentation](https://documentation.devexpress.com/WPF/17450/MVVM-Framework/Services/Services-in-custom-ViewModels).

## How to use

* Create a new interface (ie: `IMyCustomService`) for your behavior that implements `IServiceBehavior` and expose whatever you want to expose.
* Change the inheritance of an existing behavior from `Behavior<T>` to `ServiceBehavior<T>, IMyCustomService`.
* Make sure your ViewModel implements `ISupportServices`. You can see an example in [BaseViewModel.cs](https://github.com/RodgerLeblanc/ServiceBehavior/blob/master/ServiceBehavior/ViewModels/BaseViewModel.cs).
* You can now call `GetService<IMyCustomService>()` from your ViewModel to get access to your service and call whatever is exposed.


## How it works

When Services (derived from the `ServiceBehavior` class) are registered in a View, they analyze their DataContext. If the DataContext is set to an `ISupportServices` object, a service registers itself within this object. This is accomplished by calling the `IServiceContainer.RegisterService` method on the `ISupportServices.ServiceContainer` object. Thus, the service becomes available for use within the `ISupportServices` object via the `IServiceContainer.GetService<T>` method.


## Downsides

* You need to pass the `IServiceContainer` to sub-viewModels.