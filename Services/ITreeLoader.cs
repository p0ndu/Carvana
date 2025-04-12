namespace Carvana.Services;

public interface ITreeLoader // tree loader interface, to modularise and enforce loading rules
{
    Node LoadFromFile(); // synchronous loading from file for now
}