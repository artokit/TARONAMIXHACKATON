using System.Net.Http.Json;
using System.Text;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Services;
using Application.Exceptions.Cards;
using Application.Exceptions.Workers;
using Application.Services.Common.RepositoriesExtensions;
using Contracts.Cards.Responses;
using Domain.Common.Tags;
using Domain.Entities.Cards;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Application.Services;

public class CardService : ICardService
{
    private IWorkerRepository _workerRepository;
    private readonly HttpClient _httpClient;
    private IConfiguration _configuration;
    private ITagRepository _tagRepository;
    
    public CardService(IWorkerRepository workerRepository, HttpClient httpClient, IConfiguration configuration, ITagRepository tagRepository)
    {
        _workerRepository = workerRepository;
        _httpClient = httpClient;
        _configuration = configuration;
        _tagRepository = tagRepository;
    }
    
    private List<TarotCard> cards = new List<TarotCard>
        {
            new TarotCard { Id = 1, Name = "Шут", GoodPercent = 90, Meaning = GenerateMeaning(), ImageUrl = "https://assets.stickpng.com/thumbs/5ca4c08d1cf23004f2836844.png" },
            new TarotCard { Id = 2, Name = "Маг", GoodPercent = 95, Meaning = GenerateMeaning(), ImageUrl = "https://assets.stickpng.com/thumbs/5ca4c0b41cf23004f2836847.png" },
            new TarotCard { Id = 3, Name = "Верховная Жрица", GoodPercent = 85, Meaning = GenerateMeaning(), ImageUrl = "https://assets.stickpng.com/thumbs/5ca4c0c91cf23004f2836849.png" },
            new TarotCard { Id = 4, Name = "Императрица", GoodPercent = 90, Meaning = GenerateMeaning(), ImageUrl = "https://assets.stickpng.com/thumbs/5ca4c09f1cf23004f2836845.png" },
            new TarotCard { Id = 5, Name = "Император", GoodPercent = 80, Meaning = GenerateMeaning(), ImageUrl = "https://assets.stickpng.com/thumbs/5ca4c0aa1cf23004f2836846.png" },
            new TarotCard { Id = 6, Name = "Жрец", GoodPercent = 85, Meaning = GenerateMeaning(), ImageUrl = "https://assets.stickpng.com/thumbs/5ca4c1011cf23004f283684d.png" },
            new TarotCard { Id = 7, Name = "Влюбленные", GoodPercent = 70, Meaning = GenerateMeaning(), ImageUrl = "https://assets.stickpng.com/thumbs/5ca4c0ec1cf23004f283684b.png" },
            new TarotCard { Id = 8, Name = "Колесница", GoodPercent = 80, Meaning = GenerateMeaning(), ImageUrl = "https://assets.stickpng.com/thumbs/5ca4c0f61cf23004f283684c.png" },
            new TarotCard { Id = 9, Name = "Сила", GoodPercent = 90, Meaning = GenerateMeaning(), ImageUrl = "https://assets.stickpng.com/thumbs/5ca4c0bd1cf23004f2836848.png" },
            new TarotCard { Id = 10, Name = "Отшельник", GoodPercent = 75, Meaning = GenerateMeaning(), ImageUrl = "https://assets.stickpng.com/thumbs/5ca4c0db1cf23004f283684a.png" },
            new TarotCard { Id = 11, Name = "Колесо Фортуны", GoodPercent = 85, Meaning = GenerateMeaning(), ImageUrl = "https://assets.stickpng.com/thumbs/5ca4c0331cf23004f283683d.png" },
            new TarotCard { Id = 12, Name = "Справедливость", GoodPercent = 85, Meaning = GenerateMeaning(), ImageUrl = "https://assets.stickpng.com/thumbs/5ca4c03a1cf23004f283683e.png" },
            new TarotCard { Id = 13, Name = "Повешенный", GoodPercent = 50, Meaning = GenerateMeaning(), ImageUrl = "https://assets.stickpng.com/thumbs/5ca4c08d1cf23004f2836844.png" },
            new TarotCard { Id = 14, Name = "Смерть", GoodPercent = 65, Meaning = GenerateMeaning(), ImageUrl = "https://assets.stickpng.com/thumbs/5ca4c0b41cf23004f2836847.png" },
            new TarotCard { Id = 15, Name = "Умеренность", GoodPercent = 70, Meaning = GenerateMeaning(), ImageUrl = "https://assets.stickpng.com/thumbs/5ca4c0c91cf23004f2836849.png" },
            new TarotCard { Id = 16, Name = "Дьявол", GoodPercent = 40, Meaning = GenerateMeaning(), ImageUrl = "https://assets.stickpng.com/thumbs/5ca4c09f1cf23004f2836845.png" },
            new TarotCard { Id = 17, Name = "Башня", GoodPercent = 30, Meaning = GenerateMeaning(), ImageUrl = "https://assets.stickpng.com/thumbs/5ca4c0aa1cf23004f2836846.png" },
            new TarotCard { Id = 18, Name = "Звезда", GoodPercent = 90, Meaning = GenerateMeaning(), ImageUrl = "https://assets.stickpng.com/thumbs/5ca4c1011cf23004f283684d.png" },
            new TarotCard { Id = 19, Name = "Луна", GoodPercent = 60, Meaning = GenerateMeaning(), ImageUrl = "https://assets.stickpng.com/thumbs/5ca4c0ec1cf23004f283684b.png" },
            new TarotCard { Id = 20, Name = "Солнце", GoodPercent = 95, Meaning = GenerateMeaning(), ImageUrl = "https://assets.stickpng.com/thumbs/5ca4c0f61cf23004f283684c.png" },
            new TarotCard { Id = 21, Name = "Суд", GoodPercent = 80, Meaning = GenerateMeaning(), ImageUrl = "https://assets.stickpng.com/thumbs/5ca4c0bd1cf23004f2836848.png" },
            new TarotCard { Id = 22, Name = "Мир", GoodPercent = 90, Meaning = GenerateMeaning(), ImageUrl = "https://assets.stickpng.com/thumbs/5ca4c0db1cf23004f283684a.png" }
        };
    private static Random _random = new Random(Seed: 1);
    private static List<string> soft_skills = new List<string>
    {
        "Коммуникабельность", "Командная работа", "Лидерство", "Адаптивность", "Критическое мышление", "Проблемное мышление", "Эмоциональный интеллект", "Тайм-менеджмент", "Креативность",
        "Конфликтное разрешение", "Настойчивость", "Гибкость", "Стрессоустойчивость", "Мотивация", "Организованность", "Сетевые навыки", "Этика", "Интерперсональные навыки", 
        "Самоорганизация", "Способность к обучению", "Управление временем", "Доброжелательность", "Сотрудничество", "Инициативность", "Умение делегировать", "Стратегическое мышление", 
        "Наставничество", "Дипломатия", "Способность к саморефлексии", "Чувство юмора", "Управление конфликтами", "Сочувствие", "Слушание", "Умение адаптироваться", "Уверенность",
        "Целеустремленность", "Культурная осведомленность", "Способность к анализу", "Работа с разнообразием", "Умение ставить цели", "Умение работать в команде", "Умение налаживать контакты", 
        "Способность к эмпатии", "Клиентоориентированность", "Кросс-культурная коммуникация", "Способность к самоуправлению", "Долгосрочное планирование", "Умение вести переговоры", 
        "Умение адаптироваться к изменениям", "Способность к саморазвитию", "Системное мышление", "Критическая оценка", "Умение анализировать риски", "Открытость к сотрудничеству", 
        "Способность к креативному решению проблем", "Умение мотивировать других", "Эффективное взаимодействие", "Способность к принятию решений", "Умение формулировать идеи", 
        "Стратегическое планирование", "Способность к многозадачности", "Работа с обратной связью", "Способность к самоанализу", "Умение работать с информацией", "Умение вести дискуссии",
        "Способность к адаптации", "Способность к обучению на практике", "Умение делиться знаниями", "Способность к быстрому принятию решений", "Умение поддерживать позитивный настрой", 
        "Способность к конструктивной критике", "Умение организовывать рабочие процессы", "Способность к планированию", "Умение управлять ожиданиями", "Способность к интуитивному мышлению", 
        "Умение работать с разнообразием мнений", "Способность к саморазмышлению", "Эффективное использование ресурсов", "Способность к сотрудничеству", "Умение находить общий язык", 
        "Способность к долгосрочным отношениям", "Умение управлять стрессом", "Способность к позитивному мышлению", "Критическое осмысление", "Способность к командной работе", 
        "Умение устанавливать приоритеты", "Способность к конструктивному диалогу", "Умение работать с различными культурами", "Способность к эффективному взаимодействию", "Умение поддерживать рабочую атмосферу", 
        "Способность к проактивному поведению", "Умение управлять конфликтами", "Способность к быстрой адаптации", "Умение работать с клиентами", "Способность к делегированию", "Python"
    };

    public List<TarotCard> GetAllCards()
    {
        return cards;
    }

    public async Task<GenerateCompareWorkersResponseDto> GetCompareWorkers(int workerId1, int workerId2)
    {
        if (workerId1 == workerId2)
        {
            throw new EqualsWorkerNotCompareException();
        }
        
        var worker1 = await _workerRepository.GetWorkerByIdOrThrowAsync(workerId1);
        var worker2 = await _workerRepository.GetWorkerByIdOrThrowAsync(workerId2);
        var worker1Skills = await _tagRepository.GetWorkerTagsAsync(workerId1);
        var worker2Skills = await _tagRepository.GetWorkerTagsAsync(workerId2);
        var softSkills1 = worker1Skills.FindAll(i => i.Type == TagsType.Soft).Select(i => i.Name);
        var softSkills2 = worker2Skills.FindAll(i => i.Type == TagsType.Soft).Select(i => i.Name);
        if (softSkills1.Count() == 0 || softSkills2.Count() == 0)
        {
            throw new WorkerSkillsEmptyException("У работника нету софт-скиллов");
        }

        var hardSkills1 = worker1Skills.FindAll(i => i.Type == TagsType.Hard).Select(i => i.Name);
        var hardSkills2 = worker2Skills.FindAll(i => i.Type == TagsType.Hard).Select(i => i.Name);
        if (hardSkills1.Count() == 0 || hardSkills2.Count() == 0)
        {
            throw new WorkerSkillsEmptyException("У работника нету хард-скиллов");
        }

        var softs1String = string.Join(',', softSkills1.ToList());
        var softs2String = string.Join(',', softSkills2.ToList());
        var url = _configuration["AI_SERVICE_URL"] + $"/softs?tags1={softs1String}&tags2={softs2String}";
        var res = await _httpClient.GetAsync(url);
        var jsonString = await res.Content.ReadAsStringAsync();
        var reader = new JsonTextReader(new StringReader(jsonString));
        var jsonData = new JsonSerializer().Deserialize<Dictionary<string, object>>(reader);
        var worker1Card = cards.First(i => i.Id == CalculateBirthdaySum(worker1.Birthday));
        var worker2Card = cards.First(i => i.Id == CalculateBirthdaySum(worker2.Birthday));
        var normalCard = cards.First(i => i.Name.ToLower() == jsonData["name"].ToString().ToLower());
        
        var url1 = _configuration["AI_SERVICE_URL"] + $"/hards?tags1={softs1String}&tags2={softs2String}";
        var data = new
        {
            tags1 = hardSkills1.ToList(),
            tags2 = hardSkills2.ToList()
        };

        var jsonContent1 = JsonConvert.SerializeObject(data);
        
        var httpContent = new StringContent(jsonContent1, Encoding.UTF8, "application/json");

        var res1 = await _httpClient.PostAsync(url1, httpContent);
        var jsonString1 = await res1.Content.ReadAsStringAsync();
        var reader1 = new JsonTextReader(new StringReader(jsonString1));
        var jsonData1 = new JsonSerializer().Deserialize<Dictionary<string, object>>(reader1);
        
        var url2 = _configuration["AI_SERVICE_URL"] + $"/magic_compare?first_card_name={worker1Card.Name}&second_card_name={worker2Card.Name}";

        var res2 = await _httpClient.GetAsync(url2);
        var jsonString2 = await res2.Content.ReadAsStringAsync();
        var reader2 = new JsonTextReader(new StringReader(jsonString2));
        var jsonData2 = new JsonSerializer().Deserialize<List<object>>(reader2);
        
        return new GenerateCompareWorkersResponseDto
        {
            Cards = new List<TarotCard>
            {
                worker1Card,
                worker2Card,
                normalCard
            },
            ResultCompareHard = Convert.ToInt32(jsonData1["percent"]),
            MagicComparePercent = Convert.ToInt32(jsonData2[0]),
            MagicCompareText = jsonData2[1].ToString()
        };
    }

    private int CalculateBirthdaySum(DateTime birthday)
    {
        string dateString = birthday.ToString("ddMMyyyy");

        int sum = 0;
        foreach (char c in dateString)
        {
            if (char.IsDigit(c))
            {
                sum += Convert.ToInt32(c.ToString());
            }
        }

        if (sum > 22)
        {
            return Convert.ToInt32(sum.ToString()[0]) + Convert.ToInt32(sum.ToString()[1]);
        }

        return sum;
    }

    private static List<string> GenerateMeaning()
    {
        int numberOfSkills = _random.Next(5, 8);
        var meaning = new List<string>();

        for (int i = 0; i < numberOfSkills; i++)
        {
            int index = _random.Next(soft_skills.Count);
            meaning.Add(soft_skills[index]);
        }

        return meaning;
    }
}
