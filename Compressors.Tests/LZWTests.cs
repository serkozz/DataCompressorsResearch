using System.Text;

namespace Compressors.Tests;

public class LZWTests
{
    
    public const string TEST_STRING = @"Так говорила в июле 1805 года известная Анна Павловна Шерер, фрейлина и приближенная императрицы Марии Феодоровны, встречая важного и чиновного князя Василия, первого приехавшего на ее вечер. Анна Павловна кашляла несколько дней, у нее был грипп, как она говорила (грипп был тогда новое слово, употреблявшееся только редкими). В записочках, разосланных утром с красным лакеем, было написано без различия во всех:
«Si vous n'avez rien de mieux à faire, Monsieur le comte (или mon prince), et si la perspective de passer la soirée chez une pauvre malade ne vous effraye pas trop, je serai charmée de vous voir chez moi entre 7 et 10 heures. Annette Scherer» 3.
— Dieu, quelle virulente sortie! 4 — отвечал, нисколько не смутясь такою встречей, вошедший князь, в придворном, шитом мундире, в чулках, башмаках и звездах, с светлым выражением плоского лица.
Он говорил на том изысканном французском языке, на котором не только говорили, но и думали наши деды, и с теми, тихими, покровительственными интонациями, которые свойственны состаревшемуся в свете и при дворе значительному человеку. Он подошел к Анне Павловне, поцеловал ее руку, подставив ей свою надушенную и сияющую лысину, и покойно уселся на диване.
— Avant tout dites-moi, comment vous allez, chère amie? 5 Успокойте меня, — сказал он, не изменяя голоса и тоном, в котором из-за приличия и участия просвечивало равнодушие и даже насмешка.
— Как можно быть здоровой... когда нравственно страдаешь? Разве можно, имея чувство, оставаться спокойною в наше время? — сказала Анна Павловна. — Вы весь вечер у меня, надеюсь?
— А праздник английского посланника? Нынче середа. Мне надо показаться там, — сказал князь. — Дочь заедет за мной и повезет меня.
— Я думала, что нынешний праздник отменен, Je vous avoue que toutes ces fêtes et tous ces feux d'artifice commencent à devenir insipides 6.
— Ежели бы знали, что вы этого хотите, праздник бы отменили, — сказал князь по привычке, как заведенные часы, говоря вещи, которым он и не хотел, чтобы верили.
— Ne me tourmentez pas. Eh bien, qu'a-t-on décidé par rapport à la dépêche de Novosilzoff? Vous savez tout 7.
— Как вам сказать? — сказал князь холодным, скучающим тоном. — Qu'a-t-on décidé? On a décidé que Buonaparte a brûlé ses vaisseaux, et je crois que nous sommes en train de brûler les nôtres 8.
Князь Василий говорил всегда лениво, как актер говорит роль старой пиесы. Анна Павловна Шерер, напротив, несмотря на свои сорок лет, была преисполнена оживления и порывов.
Быть энтузиасткой сделалось ее общественным положением, и иногда, когда ей даже того не хотелось, она, чтобы не обмануть ожиданий людей, знавших ее, делалась энтузиасткой. Сдержанная улыбка, игравшая постоянно на лице Анны Павловны, хотя и не шла к ее отжившим чертам, выражала, как у избалованных детей, постоянное сознание своего милого недостатка, от которого она не хочет, не может и не находит нужным исправляться.
В середине разговора про политические действия Анна Павловна разгорячилась.
— Ах, не говорите мне про Австрию! Я ничего не понимаю, может быть, но Австрия никогда не хотела и не хочет войны. Она предает нас. Россия одна должна быть спасительницей Европы. Наш благодетель знает свое высокое призвание и будет верен ему. Вот одно, во что я верю. Нашему доброму и чудному государю предстоит величайшая роль в мире, и он так добродетелен и хорош, что Бог не оставит его, и он исполнит свое призвание задавить гидру революции, которая теперь еще ужаснее в лице этого убийцы и злодея. Мы одни должны искупить кровь праведника. На кого нам надеяться, я вас спрашиваю?.. Англия с своим коммерческим духом не поймет и не может понять всю высоту души императора Александра. Она отказалась очистить Мальту. Она хочет видеть, ищет заднюю мысль наших действий. Что они сказали Новосильцеву? Ничего. Они не поняли, они не могли понять самоотвержения нашего императора, который ничего не хочет для себя и все хочет для блага мира. И что они обещали? Ничего. И что обещали, и того не будет! Пруссия уже объявила, что Бонапарте непобедим и что вся Европа ничего не может против него... И я не верю ни в одном слове ни Гарденбергу, ни Гаугвицу. Cette fameuse neutralité prussienne, ce n'est qu'un piège 9. Я верю в одного Бога и в высокую судьбу нашего милого императора. Он спасет Европу!.. — Она вдруг остановилась с улыбкой насмешки над своею горячностью.
— Я думаю, — сказал князь, улыбаясь, — что, ежели бы вас послали вместо нашего милого Винценгероде, вы бы взяли приступом согласие прусского короля. Вы так красноречивы. Вы дадите мне чаю?
— Сейчас. A propos, — прибавила она, опять успокоиваясь, — нынче у меня два очень интересные человека, le vicomte de Mortemart, il est allié aux Montmorency par les Rohans 10, одна из лучших фамилий Франции. Это один из хороших эмигрантов, из настоящих. И потом l'abbé Morio; 11 вы знаете этот глубокий ум? Он был принят государем. Вы знаете?
— А! Я очень рад буду, — сказал князь. — Скажите, — прибавил он, как будто только что вспомнив что-то и особенно-небрежно, тогда как то, о чем он спрашивал, было главной целью его посещения, — правда, что l'impératrice-mère 12 желает назначения барона Функе первым секретарем в Вену? C'est un pauvre sire, ce baron, à ce qu'il paraît 13. — Князь Василий желал определить сына на это место, которое через императрицу Марию Феодоровну старались доставить барону.
Анна Павловна почти закрыла глаза в знак того, что ни она, ни кто другой не могут судить про то, что угодно или нравится императрице.
— Monsieur le baron de Funke a été recommandé à l'impératrice-mère par sa soeur 14, — только сказала она грустным, сухим тоном. В то время как Анна Павловна назвала императрицу, лицо ее вдруг представило глубокое и искреннее выражение преданности и уважения, соединенное с грустью, что с ней бывало каждый раз, когда она в разговоре упоминала о своей высокой покровительнице. Она сказала, что ее величество изволила оказать барону Функе beaucoup d'estime 15, и опять взгляд ее подернулся грустью.
Князь равнодушно замолк, Анна Павловна, с свойственною ей придворною и женскою ловкостью и быстротою такта, захотела и щелкануть князя за то, что он дерзнул так отозваться о лице, рекомендованном императрице, и в то же время утешить его.
— Mais à propos de votre famille, — сказала она, — знаете ли, что ваша дочь, с тех пор как выезжает, fait les délices de tout le monde. On la trouve belle comme le jour 16.
Князь наклонился в знак уважения и признательности.
— Я часто думаю, — продолжала Анна Павловна после минутного молчания, придвигаясь к князю и ласково улыбаясь ему, как будто выказывая этим, что политические и светские разговоры кончены и теперь начинается задушевный, — я часто думаю, как иногда несправедливо распределяется счастие жизни. За что вам дала судьба таких двух славных детей (исключая Анатоля, вашего меньшого, я его не люблю, — вставила она безапелляционно, приподняв брови), — таких прелестных детей? А вы, право, менее всех цените их и потому их не сто́ите.
И она улыбнулась своею восторженной улыбкой.
— Que voulez-vous? Lafater aurait dit que je n'ai pas la bosse de la paternité 17, — сказал князь.
— Перестаньте шутить. Я хотела серьезно поговорить с вами. Знаете, я недовольна вашим меньшим сыном. Между нами будь сказано (лицо ее приняло грустное выражение), о нем говорили у ее величества и жалеют вас...
Князь не отвечал, но она молча, значительно глядя на него, ждала ответа. Князь Василий поморщился.
— Что ж мне делать? — сказал он наконец. — Вы знаете, я сделал для их воспитания все, что может отец, и оба вышли des imbéciles 18. Ипполит, по крайней мере, покойный дурак, а Анатоль — беспокойный. Вот одно различие, — сказал он, улыбаясь более неестественно и одушевленно, чем обыкновенно, и при этом особенно резко выказывая в сложившихся около его рта морщинах что-то неожиданно-грубое и неприятное.
— И зачем родятся дети у таких людей, как вы? Ежели бы вы не были отец, я бы ни в чем не могла упрекнуть вас, — сказала Анна Павловна, задумчиво поднимая глаза.
— Je suis votre верный раб, et à vous seule je puis l'avouer. Мои дети — ce sont les entraves de mon existence 19. Это мой крест. Я так себе объясняю. Que voulez-vous?.. 20 — Он помолчал, выражая жестом свою покорность жестокой судьбе.
Анна Павловна задумалась.
— Вы никогда не думали о том, чтобы женить вашего блудного сына Анатоля. Говорят, — сказала она, — что старые девицы ont la manie des mariages 21. Я еще не чувствую за собою этой слабости, но у меня есть одна petite personne, которая очень несчастлива с отцом, une parente à nous, une princesse 22 Болконская. — Князь Василий не отвечал, хотя с свойственной светским людям быстротой соображения и памятью движением головы показал, что он принял к соображению это сведенье.
— Нет, вы знаете ли, что этот Анатоль мне стоит сорок тысяч в год, — сказал он, видимо не в силах удерживать печальный ход своих мыслей. Он помолчал.
— Что будет через пять лет, ежели это пойдет так? Voilà l'avantage d'être père 23. Она богата, ваша княжна?
— Отец очень богат и скуп. Он живет в деревне. Знаете, этот известный князь Болконский, отставленный еще при покойном императоре и прозванный прусским королем. Он очень умный человек, но со странностями и тяжелый. La pauvre petite est malheureuse comme les pierres 24. У нее брат, вот что недавно женился на Lise Мейнен, адъютант Кутузова. Он будет нынче у меня.
— Ecoutez, chère Annette 25, — сказал князь, взяв вдруг свою собеседницу за руку и пригибая ее почему-то книзу. — Arrangez-moi cette affaire et je suis votre вернейший раб à tout jamais (рап — comme mon староста m'écrit des 26 донесенья: покой-ер-п). Она хорошей фамилии и богата. Все, что мне нужно.
И он с теми свободными и фамильярными грациозными движениями, которые его отличали, взял за руку фрейлину, поцеловал ее и, поцеловав, помахал фрейлинскою рукой, развалившись на креслах и глядя в сторону.
— Attendez 27, — сказала Анна Павловна, соображая. — Я нынче же поговорю Lise (la femme du jeune Болконский) 28. И, может быть, это уладится. Ce sera dans votre famille que je ferai mon apprentissage de vieille fille 29.";

    

    /*
    public const string TEST_STRING = @"My father had an inn near the sea. It was a quiet place. One day, 
an old man came to our door. He was tall and strong, and his face 
was brown. His old blue coat was dirty and he had a big old box 
with him. He looked at the inn, then he looked at the sea. 
     My father came to the door. 
At first the old man did not speak. He looked again at the sea, 
and at the front of the inn. 
I like this place, he said. Do many people come here? No, said my father. 
Im going to stay here, said the old man. I want a bed and 
food. I like watching the sea and the ships. You can call me 
Captain.
He threw some money on the table. Thats for my bed and my 
food, he said. 
And so the old captain came to stay with us. He was always 
quiet. In the evenings he sat in the inn and in the day he watched 
the sea and the ships. 
One day he spoke to me. Come here, boy, he said, and he 
gave me some money. Take this, and look out for a sailor with         
one leg.
He was afraid of that sailor with one leg. I was afraid too. I 
looked for the man with one leg, but I never saw him. 
Then winter came, and it was very cold. My father was ill, and 
my mother and I worked very hard. 
Early one January morning, the captain went to the beach. I 
helped my mother to make the captains breakfast. The door 
opened and a man came in. His face was very white and he had 
only three ringers  on  his  left  hand.  I could see that he was a sailor. 
 Can I help you? I asked. ";
    */

    [Theory, Repeat(1)]
    public void ShouldCompressWithPositiveRatio(int count)
    {
        //Arrange
        var lzwCompressor = new LZWCompressor();

        var encodedData = lzwCompressor.Compress(Encoding.UTF8.GetBytes(TEST_STRING!));

        // Decode data
        var decodedData = lzwCompressor.Decompress(encodedData);

        var decodedText = Encoding.UTF8.GetString(decodedData);

        var compressionRatio = decodedData.Length / (float)encodedData.Length;

        Assert.Equal(TEST_STRING, decodedText);
        Assert.True(compressionRatio > 1f);
    }
}