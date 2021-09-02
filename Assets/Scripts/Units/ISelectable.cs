using System;

namespace Ziggurat
{
    interface ISelectable
    {
        bool Selectable { get; }
        bool Selected { get; }
        event Action selected;
    }
}