using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;

namespace Benjft.Loxley.ViewModels;

public class ScoreSheetRowViewModel : BaseViewModel {
    [field: AllowNull]
    [field: MaybeNull]
    public ObservableCollection<string?> Scores {
        get => field ?? throw new NullReferenceException();
        private set {
            if (field != null) {
                field.CollectionChanged -= OnScoresUpdated;
            }

            SetField(ref field, value);
            OnPropertyChanged(nameof(Length));
            OnPropertyChanged(nameof(Score));
            value.CollectionChanged += OnScoresUpdated;
        }
    } = null!;

    public int Length => Scores.Count;

    public ReadOnlyDictionary<string, int> ScoresDictionary {
        get;
        set {
            SetField(ref field, value);
            OnPropertyChanged(nameof(Score));
        }
    }

    public int Score => Scores.Sum(s => s != null ? ScoresDictionary.GetValueOrDefault(s, 0) : 0);

    private void OnScoresUpdated(object? src, NotifyCollectionChangedEventArgs args) {
        OnPropertyChanged(nameof(Scores));
        OnPropertyChanged(nameof(Length));
        OnPropertyChanged(nameof(Score));
    }

    public void SetLength(int length) {
        if (Scores.Count != 0) {
            throw new Exception("Length for scores has already been set");
        }

        Scores = new ObservableCollection<string?>(new string?[length].ToList());
    }
}
