namespace Libs.Wpf.Behaviours.Password;

using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

public class PasswordBehavior : Behavior<PasswordBox>
{
    /// <summary>
    ///     Called after the behavior is attached to an AssociatedObject.
    /// </summary>
    /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
    protected override void OnAttached()
    {
        base.OnAttached();

        this.AssociatedObject.PasswordChanged += this.OnSecurePasswordChanged;
    }

    /// <summary>
    ///     Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
    /// </summary>
    /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
    protected override void OnDetaching()
    {
        this.AssociatedObject.PasswordChanged -= this.OnSecurePasswordChanged;

        base.OnDetaching();
    }

    private void OnSecurePasswordChanged(object sender, RoutedEventArgs e)
    {
        SecurePasswordDependencyProperty.SetSecureString(
            this.AssociatedObject,
            this.AssociatedObject.SecurePassword);
    }
}
