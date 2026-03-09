using System;

namespace QbGameLib.Pool
{
    /**
     * Интерфейс объекта для пулинга
     */
    public interface IPooledObject : IDisposable
    {
        /**
         * Сброс состояния. При получении объекта из пула
         */
        void Reset();

        /**
         * Операция при возвращении объекта в пул
         */
        void Release();

        /**
         * Установка действия по возвращаению объекта в пул
         */
        void SetRealisePoolAction(Action<IPooledObject> realisePoolAction);

        /**
         * Возвращение объекта в пул
         */
        public void ReleaseInPool();

        public void SubscribeOnRelease(Action<IPooledObject> action);

    }
}