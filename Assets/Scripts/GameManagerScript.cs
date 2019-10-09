using System.Collections;
using System.Collections.Generic;
using Math;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GameManagerScript : MonoBehaviour
{
    public GameObject DropPrefab;

    private const int NrColumns = 5;
    private const int MaxDropsInColumn = 4;
    private const int BaseHeight = -1;
    private const int InitialSpeed = -20;
    private const float DropWidth = 0.8f;

    private GameObject _drop;
    private Formula _formula;
    private readonly List<int> _columns = new List<int> {0, 0, 0, 0, 0};
    private readonly List<GameObject> _drops = new List<GameObject>();
    private readonly Random _rand = new Random();
    private int _points = 0;
    private Text _pointsObject;
    private int _column;
    private bool _dropping;
    
    // Start is called before the first frame update
    void Start()
    {
        _pointsObject = GameObject.Find("Points").GetComponent<Text>();
        StartCoroutine(SpawnDrop());
    }

    // Update is called once per frame
    void  Update()
    {
        if (_dropping && _formula.CorrectAnswerGiven())
        {
            DestroyDrop();
        }

        if (_dropping && _drop.transform.position.y <= ColumnToY(_column))
        {
            LandDrop(_column);
        }

        DrawPoints();
    }

    private void DestroyDrop()
    {
        _drop.GetComponent<ParticleSystem>().Play();
        _drop.GetComponent<Renderer>().enabled = false;
        _drop.GetComponent<Rigidbody2D>().simulated = false;
        _drop.GetComponentInChildren<SpriteRenderer>().enabled = false;
        _dropping = false;
        _drop = null;
        StartCoroutine(SpawnDrop());
        _points += 1;
    }

    private void LandDrop(int column)
    {
        _drop.transform.position = new Vector3(ColumnToX(_column), ColumnToY(column));
        _drop.GetComponent<Rigidbody2D>().simulated = false;
        _drop.GetComponent<TextMesh>().text = "-";
        _columns[column] += 1;
        _dropping = false;
        _drop = null;
        if (IfFull())
        {
            Reset();
        }

        StartCoroutine(SpawnDrop());            
    }

    private void Reset()
    {
        _points = 0;
        _drops.ForEach(Destroy);
        _columns.Clear();
        _columns.AddRange(new[] {0, 0, 0, 0, 0});
    }

    private IEnumerator SpawnDrop()
    {
        yield return new WaitForSeconds(1);
        if (!IfFull())
        {
            _column = PickColumn();
            _formula = new Formula();
            _drop = Instantiate(DropPrefab, new Vector3(ColumnToX(_column), 4, 0), Quaternion.identity);
            _drop.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, InitialSpeed));
            _drop.GetComponent<TextMesh>().text = _formula.Equation;
            _drops.Add(_drop);
            _dropping = true;
        }
    }

    private void DrawPoints()
    {
        _pointsObject.text = "" + _points;
    }

    private bool IfFull()
    {
        return _columns.TrueForAll(i => i == 4);
    }
    
    private int PickColumn()
    {
        var col = _rand.Next(NrColumns);
        while (_columns[col] == MaxDropsInColumn) 
        {
            col = _rand.Next(NrColumns);
        }

        return col;
    }

    private float ColumnToX(int col)
    {
        return (_column - 2) * DropWidth;
    }

    private float ColumnToY(int col)
    {
        return BaseHeight + _columns[_column] * DropWidth;
    }

    public void ButtonClick(int number)
    {
        _formula.AddInput(number);
        _drop.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, InitialSpeed));
    }
}
