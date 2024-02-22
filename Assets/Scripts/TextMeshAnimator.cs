using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(TMP_Text))]
public class TextMeshAnimator : MonoBehaviour
{
    [SerializeField] protected float _horizontalWobble = 3.3f;
    [SerializeField] protected float _verticalWobble = 0.8f;
    protected TMP_Text _textMesh;
    protected Mesh _mesh;
    protected Vector3[] _verticies;
    protected List<int> _wordLengths = new();
    protected List<int> _wordIndexes= new(){0};

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
        EffectOverWord();
    }

    protected void GatherData()
    {
        _textMesh.ForceMeshUpdate();
        _mesh = _textMesh.mesh;
        _verticies = _mesh.vertices;
    }

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

    protected void EffectOverLetter()
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

        _mesh.vertices = _verticies;
        _textMesh.canvasRenderer.SetMesh(_mesh);
    }

    protected void EffectOverWord()
    {
        for (int w = 0; w < _wordIndexes.Count; w++)
        {
            int word = _wordIndexes[w];
            Vector3 offset = Wobble(Time.time + w);

            for (var i = 0; i < _wordLengths[w]; i++)
            {
                TMP_CharacterInfo characterInfo = _textMesh.textInfo.characterInfo[word + i];

                
                for (int j = 0; j < VerticeCount; j++)
                {
                    _verticies[j] += offset;
                }
            }
        }
        
        _mesh.vertices = _verticies;
        _textMesh.canvasRenderer.SetMesh(_mesh);
    }

    protected void EffectOverMesh()
    {
        for (var i = 0; i < _verticies.Length; i++)
        {
            Vector3 offset = Wobble(Time.time + i);
            _verticies[i] += offset;
        }

        _mesh.vertices = _verticies;
        _textMesh.canvasRenderer.SetMesh(_mesh);
    }

    private Vector2 Wobble(float time)
    {
        return new Vector2(MathF.Sin(time * _horizontalWobble), Mathf.Cos(time * _verticalWobble));
    }
}
