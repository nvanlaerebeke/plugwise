namespace Plugwise.Config; 

public class SettingsProvider {
    public ISettings Get() {
        return new Settings();
    }
}
