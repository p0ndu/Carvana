namespace Carvana.Services;

public interface ITreeLoader // tree loader interface, to modularise and enforce loading rules
{
    Node LoadFromFile(); // synchronous loading for now
    // Node LoadFromDB(); implement later
}