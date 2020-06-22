using System;

namespace Cards
{
    public interface IPlayable
    {
        void DragUp();

        void Touch();

        void DragDown();
    }
}