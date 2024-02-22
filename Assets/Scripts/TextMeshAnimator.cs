using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(TMP_Text))]
public class TextMeshAnimator : MonoBehaviour
{
    [SerializeField] protected float _horizontalWobble = 3.3f;
    [SerializeField] protected float _verticalWobble = 0.8f;
    [SerializeField] protected Gradient _rainbow;
    protected TMP_Text _textMesh;
    protected Mesh _mesh;
    protected Vector3[] _verticies;
    protected List<int> _wordLengths = new();
    protected List<int> _wordIndexes= new(){0};
    protected Color[] _colors;

    const int VerticeCount = 4;
    // Start is called before the first frame update
    private void Start()
    {
        _textMesh = GetComponent<TMP_Text>();
        PrepareWords();
    }

    private void Update()
    {
        GatherData();
        VerticePerWord();
        ColorPerWord();
        SetMeshProperties();
    }

    protected void GatherData()
    {
        _textMesh.ForceMeshUpdate();
        _mesh = _textMesh.mesh;
        _verticies = _mesh.vertices;
    }

    /// <summary>
    /// Prepares the system by separating the words by spacing.
    /// </summary>
    protected void PrepareWords()
    {
        string text = _textMesh.text;
        for (int i = text.IndexOf(' '); i > -1; i = text.IndexOf(' ', i + 1))
        {
            _wordLengths.Add(i - _wordIndexes[_wordIndexes.Count -1]);
            _wordIndexes.Add(i + 1);
        }
        _wordLengths.Add(text.Length - _wordIndexes[_wordIndexes.Count - 1]);
    }

    protected void VerticePerLetter()
    {
        for (var i = 0; i < _textMesh.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo characterInfo = _textMesh.textInfo.characterInfo[i];
            int index = characterInfo.vertexIndex;

            Vector3 offset = Wobble(Time.time + i);
            for (var j = 0; j < VerticeCount; j++)
            {
                _verticies[index + j] += offset;
            }
        }

    }

    protected void VerticePerWord()
    {
        for (int w = 0; w < _wordIndexes.Count; w++)
        {
            int word = _wordIndexes[w];
            Vector3 offset = Wobble(Time.time + w);

            for (var i = 0; i < _wordLengths[w]; i++)
            {
                TMP_CharacterInfo characterInfo = _textMesh.textInfo.characterInfo[word + i];

                int vertexIndex = characterInfo.vertexIndex;
                
                for (int j = 0; j < VerticeCount; j++)
                {
                    _verticies[vertexIndex + j] += offset;
                }
            }
        }
        
    }

    protected void ColorPerWord()
    {
        _colors = _mesh.colors;

        for (int w = 0; w < _wordIndexes.Count; w++)
        {
            int word = _wordIndexes[w];

            for (var i = 0; i < _wordLengths[w]; i++)
            {
                TMP_CharacterInfo characterInfo = _textMesh.textInfo.characterInfo[word + i];
                int index = characterInfo.vertexIndex;

                for (int j = 0; j < 4; j++)
                {
                    _colors[index + j] = _rainbow.Evaluate(Mathf.Repeat(Time.time + _verticies[index + j].x*0.001f, 1f));
                }
            }
        }
    }

    protected void VerticePerMesh()
    {
        for (var i = 0; i < _verticies.Length; i++)
        {
            Vector3 offset = Wobble(Time.time + i);
            _verticies[i] += offset;
        }

    }

    /// <summary>
    /// Call this after every modification of the verticies.
    /// </summary>

    protected void SetMeshProperties()
    {
        _mesh.colors = _colors;
        _mesh.vertices = _verticies;
        _textMesh.canvasRenderer.SetMesh(_mesh);
    }

    private Vector2 Wobble(float time)
    {
        return new Vector2(MathF.Sin(time * _horizontalWobble), Mathf.Cos(time * _verticalWobble));
    }
}
