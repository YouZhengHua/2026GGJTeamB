using Reflex.Core;
using UnityEngine;

public class RootInstaller : MonoBehaviour, IInstaller
{
    [SerializeField] private GameObject globalInitializer;
    public void InstallBindings(ContainerBuilder builder)
    {
        builder.RegisterValue("Hello"); // Note that values are always registered as singletons
        GameObject globalInstance = Instantiate(globalInitializer);
    }
}