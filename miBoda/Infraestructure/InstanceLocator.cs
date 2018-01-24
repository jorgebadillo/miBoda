

namespace miBoda.Infraestructure
{
    using ViewModels;
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            //Inicializamos Main
            Main = new MainViewModel();
        }
    }
}
