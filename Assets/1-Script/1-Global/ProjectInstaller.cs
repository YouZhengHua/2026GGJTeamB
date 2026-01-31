using Reflex.Core;
using UnityEngine;

public class ProjectInstaller : MonoBehaviour, IInstaller
{
    [SerializeField] private GameObject globalInitializer;
    public void InstallBindings(ContainerBuilder builder)
    {
        GameObject globalInstance = Instantiate(globalInitializer);
        //builder.AddSingleton(globalManagerInstance.GetComponentInChildren<MouseManager>(), typeof(MouseManager));
    }

    /*
    public void Inject(Container container)
    {
        AttributeInjector.Inject(this, container);
    }
    */
}
