using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class LoadingWindow : BaseWindow
    {
        [Header("Loading Tip")]
        [SerializeField] private TextMeshProUGUI _loadingTip;

        [Header("Loading Animation")]
        [SerializeField] private Transform _loadingIcon;

        private Sequence _loadingAnimation;

        public void Start()
        {
            StartLoadingAnimation();
            ShowRandomTip();
        }

        private void OnDestroy()
        {
            _loadingAnimation?.Kill();
        }

        private void StartLoadingAnimation()
        {
            _loadingAnimation = DOTween.Sequence()
                .Append(_loadingIcon.DORotate(new Vector3(0, 0, -360), 2f, RotateMode.FastBeyond360)
                    .SetEase(Ease.Linear)
                    .SetRelative())
                .SetLoops(-1, LoopType.Restart)
                .SetLink(gameObject);
        }

        private void ShowRandomTip()
        {
            string[] tips =
            {
                "Собирайте звезды для покупки новых кораблей!",
                "Избегайте астероидов и черных дыр!",
                "Используйте баффы для временного усиления!",
                "У вас есть 3 жизни на прохождение уровня!",
                "Черные дыры двигаются по траектории вперед-назад!",
                "Кометы оставляют за собой хвост - пролетайте через него!",
                "Энергетический барьер защитит от одного столкновения!",
                "Улучшенное топливо временно увеличивает скорость!",
                "Ремонтный набор восстановит одну потерянную жизнь!"
            };

            if (_loadingTip != null)
            {
                _loadingTip.DOFade(0, 0.3f)
                    .OnComplete(() =>
                    {
                        _loadingTip.text = tips[Random.Range(0, tips.Length)];
                        _loadingTip.DOFade(1, 0.3f);
                    });
            }
        }
    }
}