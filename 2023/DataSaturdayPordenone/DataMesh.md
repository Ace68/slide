## Data Mesh
Uno dei principi base di DDD è il modo di affrontare il problema partendo dalle esigenze del business, considerando i dati un risultato finale,
ed ora mi ritrovo a raccontare di come DDD si occupa di raccogliere i dati.

### DDD
Tra la fine del 2003 e l'inizio del 2004 Eric Evans pubblica il libro "DDD Tackling Complexity in the Heart of Software" (Blue Book), seguito, nel 2012, 
da Vaughn Vernon con il libro "Implementing Domain-Driven Design" (Red Book).
La rivoluzione che DDD ha introdotto sta proprio nel modo di affrontare il problema dello sviluppo del software introducendo alcuni pattern volti alla
creazione di una soluzione che possa durare nel tempo e crescere insieme alle esigenze del business.
Per fare questo Evans introduce alcuni pattern, alcuni strategici ed altri tattici, che aiutano noi architetti e sviluppatori nella progettazione di sistemi
che resistono alle sollecitazioni e all'invecchiamento.
Dal punto di vista strategico la proposta di Evans è proprio quella di suddivedere il problema, o meglio, il Dominio, in tanti sotto domini, ognuno con le
proprie responsabilità.
Affrontare il problema nel suo insieme di petto porta, secondo l'autore, a creare una soluzione destinata a soffocare nel suo stesso codice, perchè costretta
a soddisfare esigenze e vincoli provenienti da domini diversi e a volte, fra loro contrastanti.
Questo modo di affrontare lo sviluppo del software ha dato origine, o coumnque ha alimentato, il trend dei microservizi prima, e dei microfrontend in seguito.
Già, ma come facciamo a identificare questi sotto-domini? E soprattutto, come ci aiuta questo nella raccolta dei dati?

### Pattern Strategici
Il primo pattern introdotto da Evans per aiutarci in questa ricerca è "Ubiquitous Language"; in ogni sotto dominio esiste un vocabolario condiviso fra tutti,
dagli sviluppatori, passando per gli stackholders, sino agli utilizzatori, che non ha ambiguità. Non è affatto un linguaggio tecnico, inteso come un linguaggio
per scrivere codice, ma è altrettanto rigoroso.
Questo pattern ci aiuta ad identificare i Bounded Context, ossia i confini che delimitano ogni sotto dominio. All'interno di un Boudend Context vige lo stesso
Ubiquitous Language!
Attenzione però, l'obiettivo rimane quello di sviluppare un'applicazione nel suo insieme, non tante piccole applicazioni autonome fra loro, quindi, in un qualche modo,
questi Bounded Context dovranno essere collegati e dialogare fra loro. Per questo Evans ci mette a disposizione il pattern "Context Mapping" che ci aiuta a mappare
che tipo di relazione esiste fra un Bounded Context ed un altro.
Il pattern Bounded Context è la base per le architetture a Microservizi e, più recentemente, a Microfrontend.
Ci sarebbe molto altro da dire al riguardo, ma per il nostro scopo queste informazioni, per ora, sono
più che necessarie.

### Pattern Tattici
Una volta individuati i Bounded Context Evans ci invita a staccarci dalla visione data-centrica del problema, a favore di una visione del problema lato business.
Per essere pratici, il Cliente è visto in modo diverso dal Bounded Context che si occupa delle vendita, rispetto al Bounded Context che si occupa degli aspetti amministrativi.
Ognuno di questi Bounded Context è interessato a informazioni diverse, per questo introduce il concetto di "Entity", per sganciarci dalla logica della tabella dei
database relazionali. Ogni Bounded Context ha la propria versione della Entity Cliente, ed è responsabile della manutenzione della stessa, in pieno accordo con i
principi base della Programmazione ad Oggetti. Una Entity è l'insieme dei dati e dei comportamenti ad essa riferiti!
Alcune proprietà della Entity possono essere generiche e slegate dall'indetificativo stesso della Entity. Proviamo a pensare ad un indirizzo, inteso come via, località,
provincia, paese, etc. E' una struttura dati che può andare bene per tutte le Entity di tipo Cliente, ed infatti assume il nome di "Value Object", ossia una struttura dati
che non necessita di un identificativo, ma le cui proprietà sono immutabili.
Entitities e Value Objects formano, insieme, un Aggregato. L'aggregato esiste nel suo insieme, viene salvato, re-idratato e modificato sempre all'unisono. Ma non tutto ciò
che appartiene all'Aggregato può essere modificato liberamente dall'esterno. Evans ha previsto un rappresentante dell'Aggregato, da cui è necessario passare per modificarne lo stato.
Questo rappresentante è l'AggregateRoot, o EntityRoot.
I diversi Aggregati si scambiano informazioni passando sempre attraverso l'AggregateRoot.
Anche riguardo ai pattern tattici abbiamo solo scalfito la superficice; l'argomento è ben più vasto, ma per il nostro scopo, è più che sufficiente.

### CQRS/ES
Command Query Responsability Segregation. Questo pattern è stato introdotto da Greg Young.
In pratica riprende il concetto di Bertrand Meyer (CQS), introdotto durante la progettazione del lingaggio Eifell, uno dei cardini della OOP in generale.
Greg Young però estende il concetto e per primo intuisce l'importanza non solo di dividere concettualmente una Query di interrogazione da una di modifica del dato,
ma aggiunge la separazione dei modelli di database sottostanti le due operazioni.
Il principio base è semplice, non è possibile avere lo stesso sistema ottimizzato sia per la lettura sia per la scrittura, quindi servono due modelli di database separati,
ognuno ottimizzato per il proprio scopo.
Per quanto riguarda la scrittura Greg Young si sgancia dal concetto CRUD di Creazione e Modifica del dato, introducendo in alternativa uno store di tutti gli eventi che hanno
portato l'Aggregato ad avere lo stato corrente. E' chiaro che l'operazione di scrittura in un database in cui vengono solamente salvati gli eventi, o meglio appesi, è particolarmente
veloce. Non abbiamo bisogno di verificare se il dato esiste, recuperarlo, modificarlo e poi riscriverlo. Andiamo in append e buona notte!
Per quanto riguarda le query di lettura? E' altrettanto semplice; gli eventi emessi dal nostro Aggregato possono essere intercettati dai servizi responsabili di fornire informazioni
agli users del nostro sistema, e aggregare le informazioni in base alle specifiche esigenze della UI.
In questo modo abbiamo due sistemi ottimizzati ognuno per il proprio scopo.
Inutile ripetere che riguardo il pattern CQRS ci sarebbe da scrivere un intero libro, ma, come nei due casi precedenti, a noi interessa sapere che ci sono degli eventi, ossia classi
contenenti informazioni immutabili, a cui ci possiamo sottoscrivere per ricevere informazioni, che poi, è esattamente quello che cerchimamo come raccoglitori di dati!

## Dati
Indipendentemente dal settore in cui operiamo, o dall'Azienda in cui lavoriamo, abbiamo bisogno di raccogliere dati per costruire sistemi sempre più intelligenti per varie ragioni
  - Fornire la miglior esperienza ai clienti basata su dati personalizzati
  - Ridurre i costi e i tempi operativi attraverso ottimizzazioni basate sui dati
  - Mettere i dipendenti in condizione di poter prendere decisioni migliori grazie all'analisi delle tendenze/consumi e al business

Proviamo a ripercorrere la storia della raccolta dei dati negli ultimi 50 anni
Siamo partiti da architetture che portavano a salvare i dati in una Warehouse per passare a soluzioni più grandi, LakeHouse e finire, ad oggi, nel cloud avendo a disposizione
storage più grandi, ma sempre in un'ottica molotica della raccolta dati.
Intendiamoci, la soluzione a monolite è il modo perfetto per partire velocemente a produrre del valore tangibile, questo non solo nella raccolta dei dati. Ma pone dei vincoli
nella scalabilità e nell'elasticità al cambiamento.
Qual'è il modo migliore per calare un monolite? Tecnicamente parlando è quello di decomporlo
Possiamo operare una decomposizione tecnologica, ossia separare i vari processi che ci portano ad ottenere l'attuale risultato finale. Questo ci consentirà sicuramente
una maggior scalabilità a fronte del fatto che potremo distribuire i processi su macchine diverse.
Ma sappiamo tutti che le necessità, le richieste da parte del business, o dei nostri Clienti, non sono mai lineari, sono piuttosti ortogonali ai nostri processi, quindi
una separazione per processi andrà sicuramente a scontrarsi con i Team, o il Team, che dovranno gestire il valore da distribuire agli utenti finali.
E allora? Proviamo a creare Team di Data Engineers, o ML engineers super specializzati, dei silos per ogni richiesta. Ma in questo momento, nuovamente, perderemmo di vista
la visione d'insieme dell'intero Dominio, a scapito della qualità dei risultati forniti. Quelli bravi nella progettazione di modelli di ML direbbero che il rischio è di over-fittare
i nostri modelli.



Molte delle complessità tecniche che le organizzazioni si trovano ad affrontare oggi derivano dal modo in cui abbiamo diviso i dati.
Ci sono diversi sintomi che si possono notare che denotano alcune problematiche che impediscono il successo dell'attuale modalità di gestione dei dati
  - Fail to boostrap

Dati Analitici e Dati Operativi. Come abbiamo isolato i Team che li gestiscono, gli stack tecnologici che li supportano e come questi li supportano e li integrano.

Data Mesh riconosce la differenza fra Dati Operativi e Dati Analitici ed introduce un nuovo modello di stretta integrazione tra i due, rispettandone le differenze.

### Dati Operativi
  - **Gestione del Business**
  - **Servire gli Utenti**


I Dati Operativi sono i dati salvati nei database dei microservizi delle nostre applicazioni ed hanno la responsabilità di mantenere lo stato corrente dei business.
Sono dati fondamentali per le scelte continue che gli utilizzatori dei nostri software devono continuamente fare.
Questi dati sono assolutamente privati, quindi non accessibili da altri servizi.
Cosa condivere con il mondo esterno è responsabilità del microservizio. In che modo? Tramite eventi, in particolare tramite Integration Event.

### Dati Analitici
Rappresentano la visione storica, integrata e aggregata dei dati creati come prodotto secondario dalla gestione aziendale.
Viene mantenuto e utilizzato dai sistemi OLAP (online analytical processing).
I dati analitici sono la visione temporale, storica e spesso aggregata dei fatti dell’azienda nel tempo. Vengono modellati per fornire informazioni retrospettive o future.
I dati analitici sono ottimizzati per la logica analitica, l’addestramento di modelli di apprendimento automatico e la creazione di report e visualizzazioni all’esterno”,
ovvero i dati a cui accedono direttamente i consumatori analitici.

### Le Dimensioni del Cambiamento
Data Mesh introduce alcuni cambiamenti, sia tecnici che organizzativi, rispetto al precedente approccio alla gestione dei dati.
Organizzazione: abbandona l'approccio centralizzato della gestione del dato in favore di una decentralizzazione rispetto al Dominio di appartenenza
Architetturale: basta con la struttura monolitica dei Data Lake, in favore di una rete di data products distribuiti
Tecnologicamente: basta trattare il dato come un prodotto secondario; il dato analitico è frutto di pipeline dedicate
Operativamente: sposta la governancedel dato da una gestione operativa centralizzata, con interventi umani, verso un modello federato, con politiche computazionali nei nodi della rete
Principi: sposta il concetto da dato come asset aziendale, a dato come prodotto, interno ed esterno all'organizzazione
Infrastrutturalmente: distinzione fra dati operativi e dati analitici, ma con un sistema ben integrato diinfrastrutture

Negli ultimi anni le aziende hanno dato il via ad una progressiva, ed inevitabile, trasformazione digitale. Stiamo assistendo ad una migrazione di massa verso il Cloud Computing,
e questa migrazione a enfatizzato alcune architetture, quali i microservizi prima ed i microfrontend in seguito, che a loro volta hanno trasformato il modo di lavorare di interi Team
di sviluppatori, come ci ha insegnato Team Topologies, ed ovviamente, anche il mondo dei dati, non poteva restare fermo al palo ad osservare. Una trasformazione, se non altro per poter
evolvere, era necessaria. Tutte queste trasformazioni sono state guidate da alcuni principi, che proveremo ad analizzare insieme.

### Principal of Domain Ownership
Ispirandosi al concetto di Dominio espresso in DDD i dati vengono organizzati in Domini; in senso stretto un Dominio comprende una serie di dati omogenei rispetto ad alcuni criteri che possono essere l’origine, l’aggregazione ed il consumo degli stessi. In questo approccio ci sono diversi aspetti che possiamo definire rivoluzionari rispetto al modo tradizionale di raccolta dati. Innanzitutto il processo di gestione dei dati viene posto sotto la ownership di un team interdisciplinare di attori tutti operanti nella sfera di competenza di questi dati, ed ancora si nota l’influenza di DDD in questo principio rimarcando l’importanza dei Business Expert per poter comprendere e sfruttare pienamente le potenzialità.

Questa differenziazione rispetto ad un modello a Data Lake, che vede il convogliamento di tutti i dati operativi in unico ambiente, riduce l’entropia informativa derivante dal fatto che le figure che presidiano il Data lake stesso non possono essere esperte di tutti i domini interessati.

Le motivazioni che supportano questo principio sono diverse

    La capacità di scalare la condivisione dei dati allineata con gli assi della crescita organizzativa: aumento del numero delle fonti di dati, aumento del numero dei consumatori di questi stessi dati e aumento della diversità dei casi d’uso di conusmo degli stessi.
    Ottimizzazione del cambiamento continuo grazie alla localizzazione del cambiamento nei domini aziendali
    Supporto all’agilità riducendo la necessità di sincronizzazione tra i team rimuovendo i colli di bottiglia nell’avere un team centralizzato per la raccolta dati che faccia riferimento ad un unico Data Lake
    Aumentare la resilienza delle soluzioni di machine learning rimuovendo complesse pipeline di raccolta dati intermedie

Il rischio che si corre scomponendo i dati per dominio è quello di creare dei silos, rischiando di compromettere l’integrazione e la coerenza complessiva. Ma per evitare questo ci sono altri principi da seguire.

### Principal of Data as a Product
Un pò come il Context Mapping risolve il rapporto di comunicazione fra i vari Bounded Context il Principio di Dati come Prodotto indica la modalità con cui i singoli domini interagiscono per garantire una gestione fluida ed efficiente dei processi aziendali. Abbiamo già detto che Data Mesh è orientato a gestire dati analitici, ossia dati che derivano da aggregazioni e/o integrazioni, ma va anche precisato che questi dati possono avere finalità differenti, ad esempio, rendicontazione interna tramite dashboard e/o report, analisi e ricerca, magari sfruttando modelli di Machine Learning, o persino supporto a processi operativi a valle degli stessi che li hanno generati.

Per Data Product si intende una collezione di dati, corredata dal codice necessario al relativo consumo, e dei metadati che ne descrivono contenuto, accuratezza, età, fonti, ownership, modalità di consumo, insomma delle relative caratteristiche. E’ realizzato all’interno del Dominio dei Dati, ed è destinato sia al consumo interno dal Dominio stesso, ma anche al consumo da parti degli altri Domini, grazie ad catalogo centrale che censisce tutti i data product e assicura l’adeguata interoperabilità fra i Domini.

Per poter garantire tutto questo, un Data Product deve rispondere ad un set minimo di otto criteri, per cui deve essere

  - Usable, Valuable, Feasible (Marty Cagan - Inspired)
  - Discoverability, Understanding  (Don Norman - The design of every day things)
    -- Discoverability: figure out what actions are possible and where and how to perform them?
    -- Understanding: What does it all means? How is supposed to be used? What do all different controls and settings mean?
  - Trusthful (trustworthy) (Rachel Botsman - A confident relationship with the unknown - Trust Thinkers)
  - Interoperable, Natively Accessible

Nascono nuove figure
  Data Product Owner
  Data Product Developer

La coesistenza di dati e codice non è una novità assoluta per chi si è già approcciato al mondo dei microservizi.
La chiave di lettura per comprendere a fondo il concetto di Dato come un Prodotto sta in questi punti
  In un microservizio il dato è al servizio del codice
  In Data Mesh il codice è al servizio del dato

### Principal of the Self-Serve Data Platform
Aspettarsi che i team di ingegneria del dominio possiedano e condividano i dati analitici come un prodotto,
oltre alla costruzione di applicazioni e alla manutenzione di prodotti digitali, solleva legittime preoccupazioni sia per i professionisti che per i loro leader.
Domande del tipo
"Come farò a gestire i costi di gestione dei prodotti di dati di dominio, se ogni dominio deve costruire e possedere i propri dati?"
"Come faccio ad assumere gli ingegneri dei dati, che sono già difficili da trovare, per il personale di ogni dominio?".
  "Mi sembra che ci sia un'eccessiva ingegneria e di sforzi duplicati in ogni team"
"Come possiamo estendere la responsabilità del nostro team non solo per la creazione di applicazioni per il funzionamento dell'azienda, ma anche per la condivisione dei dati?

Per rispondere a queste domande esiste appunto il terzo princio del Data Mesh, ossia "L'infrastruttura dei Dati self-service come piattaforma".
Le piattaforme di dati e di analisi non mancano, hanno solo bisogno di essere modificate in modo che possano scalare la condivisione, l'accesso e l'utilizzo dei dati analitici,
in modo decentralizzato, per una nuova generazione di tecnologi generalisti.
Ci sono diverse tecnologie che ricadono sotto l'etichetta di "data infrastructure", alcune di esse sono
  - Analytical Data Storage, nella forma di Data Lake, (Warehouse o Lakehouse)
  - Data Processing framework, per processare dati in modalità batch
  - Data Query Languages, basate fondamentalmente su script SQL-like
  - Pipeline workflow management, per orchestrare task complessi di data pipeline o modelli di ML.

La responsabilità maggiore di Data Mesh è quella di consentire ai Team esistenti, o i nuovi Team, di costruire, condividere e utilizzare data-products; acquisire dati da altre fonti;
trasformare e condividere dati come prodotti con l'utilizzatore finale.
La piattaforma deve consentire ai Team di svolgere queste attività in modo autonomo, senza alcuna dipendenza da Team centralizzati o da intermediari.
Molte tecnologie di gestione dati esistenti sono concepite con il presupposto di un Team decentralizzato che acquisisce, gestisce e condivide i dati per tutti i domini.
Questa concezione comporta alcune conseguenze, tecniche ed economiche
  - I costi sono stimati e gestiti in maniere monolitica, e non isolati per Dominio
  - La sicurezza e la gestione della privacy sono condivise con la/le risorse che devono acquisire e gestire i dati, non isolare per contesto
  - La gestione delle pipeline viene affidata ad un unico Team, in contrasto con la concezione di avere pipeline indipendenti, allocate sul prodotto / Dominio.


### Principal of Federated Computational Governance
Nasce la necessità di abbracciare un nuovo sistema di valori, che abbracci il decentramento o la sovranità del dominio.
I vari sotto-domini sono hanno il controllo dei loro dati, decidono come modellarli e cosa condividere. Le modalità di condivisione vanno condivise
Pag. 110/387


## Why Data Mesh?
I cambiamenti costano, e Data Mesh non è un'eccezione. Ma allora perchè un'Azienda dovrebbe cambiare approccio?
Possiamo vedere Data Mesh come un punto di flesso; matematicamente parlando, visto che dati e matematica si accompagnano volentieri,
un punto di flesso è un momento magico in cui una linea smette di curvare in un senso ed inizia a curvare nell'altro. Un punto in cui
la vecchia immagine lascia il posto a quella nuova.
Ovviamente non sto dicendo che Data Mesh è l'ultima frontiera di questo cambiamento, altre ne vedremo in futuro, ma è quella che è certamente
più rilevante in questo momento. Un pò come diceva l'agente Smith in Matrix, e come i dati di questo studio confermano, le Aziende che hanno
iniziato ad adottare Data Mesh, hanno maggiori possibilità di raggiungere nuovi obiettivi. L'approccio più Agile, più snello, nella gestione dei
dati consente risposte più rapide alle richieste di chi questi dati li consuma quotidianamente, ed ha bisogno continuamente di cambiamenti ed innovazioni.