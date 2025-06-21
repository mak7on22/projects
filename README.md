📋 Список фич (1 – 20) и их краткое назначение
#	Фича	Суть / зачем нужна
1	Авторизация/регистрация	ASP.NET Core Identity (вход по e-mail/паролю, внешний OAuth: Google, VK, Discord). [bearbones] (Program.cs, Models/User.cs, Models/ApplicationDbContext.cs)
2	Личный кабинет	Профиль, аватар, баланс монет, история побед, настройки безопасности. [bearbones] (Pages/Profile/Index.cshtml)
3	Каталог мини-игр	Страница со списком игр по жанрам; быстрый запуск. [bearbones] (Pages/Games/Index.cshtml)
4	Счётчик побед по каждой игре	Отдельная статистика, обновляется после окончания партии. [partially] (Models/GameWinStat.cs, Servises/MatchService.cs, Pages/Games/Stats.cshtml)
5	Награда внутриигровыми монетами	Автоматическое начисление X монет за победу, анти-флуд таймер. [bearbones] (Servises/MatchService.cs)
6       “Донатные” (премиум) монеты     Вторая валюта, покупается за реальные деньги (Stripe / YooKassa). [bearbones] (Models/PremiumTransaction.cs, Servises/PremiumService.cs)
7       Магазин донатов Покупка наборов премиум-монет, VIP-подписка, бустеры опыта. [bearbones] (Pages/Shop/Index.cshtml)
8	Игры на логическое мышление	Sudoku, Nonograms, 2048, Мини-шахматы и др.

Дополняем до 20:

#	Фича	Суть / зачем нужна
9	Ежедневные и недельные квесты	Доп. способ заработать монеты и удержать аудиторию.
10	Достижения (Achievements)       Значки за первые 10 побед, безошибочный раунд и т.д. [bearbones] (Models/Achievement.cs, Models/UserAchievement.cs, Servises/AchievementService.cs, Pages/Achievements/Index.cshtml)
11	Глобальный и дружественный лидерборд    Топ-100 по победам, фильтр “друзья”. [bearbones] (Pages/Leaderboard/Index.cshtml)
12	Магазин внутриигровых предметов	Скины досок/тем, подсказки, купить за обычные или донатные монеты.
13	Обмен внутриигровых → донатных монет (ограниченный)	Лояльность free-to-play игрокам, баланс экономики.
14	Матч-мейкинг PvP (опционально)	Подбор соперника по ELO, режим “битва умов”.
15	Реальный-тайм чат и эмодзи в игре	SignalR; коммуникация повышает вовлечённость.
16	Админ-панель	Управление играми, банами, предметами магазина, акциями.
17	Аналитика и события	Интеграция App Insights / Google Analytics, custom events.
18	Система пуш-уведомлений / e-mail	“Заберите ежедневную награду”, обновления, акции.
19	CI/CD, автотесты и нагрузочное тестирование	GitHub Actions + Playwright / xUnit; устойчивость к пиковым играм.
20	Защита и безопасность	HTTPS, CSP, rate-limit, 2FA, защита от читов (проверка ходов на сервере). [bearbones]

🔧 Рекомендуемая последовательность разработки
Этап	Крупные задачи	Ключевые технологии
0. Подготовка   • Определить доменную модель (User, Game, Match, Wallet). [fully]
• Настроить репозиторий, CI, базу данных (PostgreSQL).	GitHub Actions, EF Core, PostgreSQL [partially]
1. Базовая платформа	1–2–3 из таблицы: Auth + кабинет + каталог игр.	ASP.NET Core Identity, Razor Pages/Blazor, Tailwind [bearbones]
2. Игровая логика	• Реализовать 1-ю игру (например, Sudoku) как Proof of Concept.
• Общий эндпоинт расчёта побед/поражений.	Blazor WASM (UI), сервер API
3. Экономика	• Счётчик побед (4) и награда монетами (5).
• База для прем-валюты (6) без оплаты.	EF Core миграции
4. Монетизация	• Магазин донатов (7).
• Интеграция платежного шлюза.	Stripe .NET SDK / YooKassa SDK
5. Контент-апдейт	• Добавить ещё 2–3 логических игры (8).
• Ежедневные квесты (9), достижения (10).	Серверные Cron-задачи (Quartz)
6. Социальный слой	• Лидерборд (11), чат (15).
• Match-making (14) при наличии PvP.	SignalR, Redis pub/sub
7. Магазины и обмен	• Витрина внутриигровых предметов (12).
• Обмен валют (13) с лимитами.	DDD + Domain Events
8. Администрирование	• Админ-панель (16).
• Права ролей, журнал действий.	Blazor/React admin template
9. Оптимизация и аналитика	• Инструменты мониторинга (17).
• A/B-тесты акций.	App Insights, Feature Flags
10. Обкатка и релиз	• UI-полировка, адаптивность.
• Pentest, нагрузочные тесты (19, 20).	k6, OWASP ZAP
11. Post-launch	• Пуш-уведомления (18).
• Регулярные контент-апдейты, новые игры.	Web Push, Background Jobs

🛠️ Техстек (кратко)
Backend: ASP.NET Core 9, C#, EF Core, PostgreSQL, Redis.

Frontend: Blazor (Server или WASM) или React + ASP.NET Core API.

Real-time: SignalR.

Payments: Stripe / YooKassa.

DevOps: Docker, GitHub Actions, k8s (при росте).

Инфрастуктура мониторинга: Application Insights, Grafana, Prometheus.

⚡ Советы по организации работы
Инкрементные релизы: выкладывайте MVP каждые 2-3 недели, фидбек → итерация.

Feature flags: включайте новые игры, магазин, PvP поэтапно, чтоб не ломать прод.

Баланс экономики: заранее задайте формулы наград, стоимость предметов, капы обмена; используйте конфиги в БД, а не хардкод.

Тест-драйв: внутренняя альфа-группа 5–10 человек → закрытая бета → публичный ранний доступ.

Документация: Swagger/OpenAPI для API, Wiki для бизнес-правил игр.

## Progress
- Созданы базовые сущности и контекст EF Core (User, Game, Match, Wallet) – **fully**.
- Подключена база данных PostgreSQL в настройках и сервисах – **partially** (без CI).
- Настроена базовая аутентификация через ASP.NET Core Identity – **bearbones** (Program.cs, Models/User.cs, Models/ApplicationDbContext.cs).
- Создана страница каталога игр – **bearbones** (Pages/Games/Index.cshtml).

- Реализована страница профиля пользователя – **bearbones** (Pages/Profile/Index.cshtml).
- Добавлен MatchService для записи результатов и награды монетами – **bearbones** (Servises/MatchService.cs).

- Добавлена таблица статистики побед GameWinStat и страница просмотра результатов – **partially** (Models/GameWinStat.cs, Pages/Games/Stats.cshtml, Servises/MatchService.cs)

- Добавлены премиум транзакции и базовый магазин донатов – **bearbones** (Models/PremiumTransaction.cs, Pages/Shop/Index.cshtml, Servises/PremiumService.cs)
- Добавлена система достижений и страница списка достижений – **bearbones** (Models/Achievement.cs, Models/UserAchievement.cs, Servises/AchievementService.cs, Pages/Achievements/Index.cshtml)
- Реализован глобальный лидерборд побед – **bearbones** (Pages/Leaderboard/Index.cshtml)
