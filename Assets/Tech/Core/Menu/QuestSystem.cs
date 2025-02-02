using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSystem : MonoBehaviour
{
    [SerializeField] private RectTransform questListContainer;
    [SerializeField] private GameObject questItemPrefab;
    [SerializeField] private Color strikeThroughColor;

    private List<Quest> quests = new();

    private void Start()
    {
        AddQuest("Выбраться из зоопарка");
        AddQuest("Найти оружие");
    }

    public void AddQuest(string description)
    {
        if (quests.Exists(q => q.Description == description)) return;

        Quest newQuest = new() { Description = description, IsCompleted = false };
        quests.Add(newQuest);

        GameObject questItem = Instantiate(questItemPrefab, questListContainer);
        Text questText = questItem.GetComponentInChildren<Text>();
        questText.text = "- " + description;

        GameObject strikeThrough = new("StrikeThrough", typeof(Image));
        strikeThrough.transform.SetParent(questItem.transform);
        Image strikeImage = strikeThrough.GetComponent<Image>();
        strikeImage.color = strikeThroughColor;

        RectTransform strikeRect = strikeThrough.GetComponent<RectTransform>();
        strikeRect.anchorMin = new Vector2(0, 0.5f);
        strikeRect.anchorMax = new Vector2(0, 0.5f);
        strikeRect.sizeDelta = new Vector2(0, 2);
        strikeRect.anchoredPosition = Vector2.zero;
        strikeRect.localScale = Vector2.one;
        strikeThrough.SetActive(false);

        newQuest.UIElement = questItem;
        newQuest.StrikeThrough = strikeThrough;
        newQuest.StrikeImage = strikeImage;

        LayoutRebuilder.ForceRebuildLayoutImmediate(questListContainer.GetComponent<RectTransform>());
    }

    public void CompleteQuest(string description)
    {
        Quest quest = quests.Find(q => q.Description == description);
        if (quest != null && !quest.IsCompleted)
        {
            quest.IsCompleted = true;

            Text questText = quest.UIElement.GetComponentInChildren<Text>();
            questText.color = Color.gray;

            quest.StrikeThrough.SetActive(true);
            StartCoroutine(AnimateStrikeThrough(quest.StrikeImage.rectTransform));
        }
    }

    private System.Collections.IEnumerator AnimateStrikeThrough(RectTransform strikeRect)
    {
        float duration = 1.5f;
        float elapsedTime = 0f;
        Vector2 startAnchorMax = new(0, 0.5f);
        Vector2 endAnchorMax = new(1, 0.5f);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            strikeRect.anchorMax = Vector2.Lerp(startAnchorMax, endAnchorMax, t);
            yield return null;
        }

        strikeRect.anchorMax = endAnchorMax;
    }

    public void RemoveQuest(string description)
    {
        Quest quest = quests.Find(q => q.Description == description);
        if (quest != null)
        {
            Destroy(quest.UIElement);
            quests.Remove(quest);
        }
    }

    private class Quest
    {
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public GameObject UIElement { get; set; }
        public GameObject StrikeThrough { get; set; }
        public Image StrikeImage { get; set; }
    }
}
