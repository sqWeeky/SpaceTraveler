using TMPro;
using UnityEngine;

namespace SpaceTraveler.Scripts.Players
{
    public class PlayerLabel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _rank;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _score;
        
        public void Fill(int rank, string name, int score)
        {
            _rank.text = rank.ToString();
            _name.text = name;
            _score.text = score.ToString();
        }
    }
}