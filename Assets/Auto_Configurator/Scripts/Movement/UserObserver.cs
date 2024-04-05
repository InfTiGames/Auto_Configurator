using System;

public class UserObserver : IDisposable
{
    private readonly CarSubject _carSubject;
    private bool _disposed = false;

    public bool PossibleToMove { get; private set; } = true;

    public UserObserver(CarSubject carSubject)
    {
        _carSubject = carSubject != null ? carSubject : throw new ArgumentNullException(nameof(carSubject));
        _carSubject.OnCarClicked += CanMove;
    }

    private void CanMove()
    {
        PossibleToMove = !_carSubject.IsSelected;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                if (_carSubject != null)
                {
                    _carSubject.OnCarClicked -= CanMove;
                }
            }

            _disposed = true;
        }
    }
}