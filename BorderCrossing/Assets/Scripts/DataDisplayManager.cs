using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Takes care of displaying the data collected from the players.
/// </summary>
public class DataDisplayManager : MonoBehaviour
{
    #region Exposed Variables

    [Tooltip("Sprite used as an empty field of the graph.")]
    [SerializeField] private Sprite empty;
    [Tooltip("Sprite used as a filled field of the graph.")]
    [SerializeField] private Sprite filled;
    [Tooltip("Size of a scale on the graph. Same as the slider in the MainGraphScene.")]
    [SerializeField] private int scale = 10;
    [Tooltip("Scriptable object containing the list of BoundaryData collected from the players.")]
    [SerializeField] private BoundaryDataList dataList;
    [Space(10)] [Tooltip("If true, the debug messages will be displayed.")]
    [SerializeField] private bool isDebug = false;
    
    #endregion

    #region Private Variables

    private readonly List<Dictionary<int, int>> _listOfValuesWithAppearances = new();
    private readonly List<GameObject> _graph = new();
    private int _questionNumber;

    #endregion

    private void OnEnable()
    {
        if (dataList == null || dataList.ReadData().Count == 0)
        {
            if(isDebug) Debug.LogError("No data available!");
            return;
        }

        for (int i = 0; i < dataList.ReadData()[0].data.Count; i++)
        {
            _listOfValuesWithAppearances.Add(ReadScores(dataList.ReadData(), i));
        }

        GenerateDataDisplay(0);
    }

    /// <summary>
    /// Destroys the graph and creates the new one for another question.
    /// </summary>
    private void RegenerateGraph()
    {
        foreach (var field in _graph)
        {
            Destroy(field);
        }

        _graph.Clear();
        GenerateDataDisplay(_questionNumber);
    }

    /// <summary>
    /// Switches to next data set and regenerates the graph.
    /// </summary>
    public void NextQuestion()
    {
        if (_questionNumber + 1 >= dataList.ReadData()[0].data.Count - 1)
        {
            _questionNumber = dataList.ReadData()[0].data.Count - 1;
        }
        else
        {
            _questionNumber++;
        }
        
        RegenerateGraph();
    }

    /// <summary>
    /// Switches to previous data set and regenerates the graph.
    /// </summary>
    public void PreviousQuestion()
    {
        if (_questionNumber - 1 < 0)
        {
            _questionNumber = 0;
        }
        else
        {
            _questionNumber--;
        }
        
        RegenerateGraph();
    }

    /// <summary>
    /// Generates sprites to display the data visually.
    /// </summary>
    /// <param name="questionNumber">Number of question that the data should be displayed for.</param>
    private void GenerateDataDisplay(int questionNumber)
    {
        for (int i = 0; i < dataList.ReadData().Count; i++)
        {
            for (int j = 0; j < scale; j++)
            {
                GameObject field = new GameObject("Field");
                field.transform.SetParent(transform);
                field.transform.localPosition = new Vector3(1 * j, 1 * i, 0);
                field.transform.localRotation = Quaternion.identity;
                SpriteRenderer fieldSprite = field.AddComponent<SpriteRenderer>();
                _graph.Add(field);
                if (_listOfValuesWithAppearances[questionNumber].Keys.Contains(j) &&
                    _listOfValuesWithAppearances[questionNumber][j] >= (i + 1))
                {
                    fieldSprite.sprite = filled;
                    fieldSprite.color = Color.red;
                }
                else
                {
                    fieldSprite.sprite = empty;
                }
            }
        }
    }

    /// <summary>
    /// Read the data set uploaded, count how many times it appears, add it into the dictionary.
    /// </summary>
    /// <param name="data">Data that needs to be sorted into the dictionary.</param>
    /// <param name="questionNumber">Number of question that the data should be counted for.</param>
    /// <returns>Dictionary with the number of how many times specific answers appear on given question.</returns>
    private Dictionary<int, int> ReadScores(List<BoundaryData> data, int questionNumber)
    {
        Dictionary<int, int> valueWithNumberOfAppearance = new Dictionary<int, int>();
        foreach (var boundaryData in data)
        {
            var scoreToInt = boundaryData.data[questionNumber];
            if (!valueWithNumberOfAppearance.Keys.Contains(scoreToInt))
            {
                valueWithNumberOfAppearance.Add(scoreToInt, 1);
            }
            else
            {
                valueWithNumberOfAppearance[scoreToInt]++;
            }
        }

        if (isDebug)
        {
            foreach (var valuePair in valueWithNumberOfAppearance)
            {
                Debug.Log($"Value {valuePair.Key} appears {valuePair.Value} times");
            }
        }

        return valueWithNumberOfAppearance;
    }
}