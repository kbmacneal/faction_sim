INSERT INTO assets ("Name",hp,"Attack","Counterattack","Id","AttackStats","AttackDice","DefenderReroll","AttackerReroll","AttackerExtraDice","DefenderExtraDice","Description","Type") VALUES 
('Franchise','3','WvW, 1D4','1d4-1',0,'WvW','1D4','False','False','False','False','Franchise assets reflect a deniable connection with a local licensee for the faction’s goods and services. When a Franchise successfully attacks a enemy asset, the enemy faction loses one FacCred (if available), which is gained by the Franchise’s owner. This loss can happen only once a turn, no matter how many Franchises attack.','Wealth')
,('Harvesters','4','None','1d4',1,'None','None','False','False','False','False','Harvesters gather the natural resources of a world, whether ore, biologicals, or other unprocessed natural goods. As an action, the Harvesters’ owning faction may roll 1d6. On 3+, one FacCred is gained.','Wealth')
,('Local Investments','2','WvW, 1d4-1','None',2,'WvW','1d4-1','False','False','False','False','Local Investments give the faction substantial influence over the commerce on a world. Any other faction that tries to buy an asset on that planet must pay one extra FacCred. This money is not given to the investments’ owner, but is lost. This penalty is only applied once, no matter how many Local Investments are present','Wealth')
,('Base Of Influence','1','None','None',3,'None','None','False','False','False','False','Base of Influence assets are special, and are required for purchasing or upgrading units on a particular world. Any damage done to a Base of Influence is also done to a faction’s hit points. The cost of a Base of Influence equals its maximum hit points, which can be any number up to the total maximum hit points of its owning faction. A faction’s bases of influence don’t count against their maximum assets. A Base of Influence can only be purchased with the “Expand Influence” action, and not with a normal “Buy Asset” action','Special')
,('Freighter Contract','4','WvW, 1D4','None',4,'WvW','1D4','False','False','False','False','Freighter Contract assets grant special links with heavy shipping spacers. As an action, the faction may move any one non-Force asset including this one- to any world within two hexes at a cost of one FacCred.','Wealth')
,('Lawyers','4','CvW, 2d4','1d6',5,'CvW','2d4','False','False','False','False','Lawyers as universal, whether sophists in immaculate suits or charismatic tribal skalds. Lawyers have the ability to tie an enemy up in the knots of their own rules, damaging assets with confusion and red tape. Lawyers cannot attack or  counterattack Force assets.','Wealth')
,('Union Toughs','6','WvF, 1d4+1','1d4',6,'WvF','1d4+1','False','False','False','False','Union Toughs don’t much like scabs and management, and they’re willing to take the faction’s word on which people are which. They’re lightly armed and poorly trained, but they can often infiltrate to perform sabotage when necessary.','Wealth')
,('Surveyors','4','None','1d4',7,'None','None','False','False','False','False','Surveyors explore potential resource and investment options on worlds. The presence of a Surveyor crew allows one additional die to be rolled on Expand Influence actions. As an action, a surveyor crew can be moved to any world within two hexes.','Wealth')
,('Postech Industry','4','None','1d4',8,'None','None','False','False','False','False','Postech Industry produces a wide range of postech products out of local resources. As an action, the owning faction can roll 1d6 for a Postech Industry asset. On a 1, one  FacCred is lost, on a 2-4 one FacCred is earned, and a 5-6 returns two FacCreds. If money is lost and no resources are available to pay it, the Postech Industry is destroyed','Wealth')
,('Laboratory','4','None','None',9,'None','None','False','False','False','False','Laboratory assets allow a world to make hesitant progress in tech. The presence of a Laboratory allows assets to be purchased on that world as if it had tech level 4, at +2 FacCreds cost.','Wealth')
;
INSERT INTO assets ("Name",hp,"Attack","Counterattack","Id","AttackStats","AttackDice","DefenderReroll","AttackerReroll","AttackerExtraDice","DefenderExtraDice","Description","Type") VALUES 
('Mercenaries','6','WvF, 2d4+2','1d6',10,'WvF','2d4+2','False','False','False','False','Mercenaries are groups of well-equipped, highly-trained soldiers willing to serve the highest bidder. Mercenaries have a maintenance cost of one FacCred per turn. As an action, Mercenaries can move to any world within one hex of their current location. To purchase or move a Mercenary asset to a planet requires government permission','Wealth')
,('Shipping Combine','10','None','1d6',11,'None','None','False','False','False','False','Shipping Combine assets allow the transport of large amounts of equipment and personnel between worlds. As an action, the combine may move any number of non-Force assets- including itself- to any world within two hexes at a cost of one FacCred per asset.','Wealth')
,('Monopoly','12','WvW, 1d6','1d6',12,'WvW','1d6','False','False','False','False','Monopoly assets involve an open or tacit stranglehold on certain vital businesses or resources on a world. As an action, owners of a monopoly may force one other faction with unstealthed assets on that world to pay them one FacCred. If the target faction hasn’t got the money, they lose one asset of their choice on the world.','Wealth')
,('Medical Center','8','None','None',13,'None','None','False','False','False','False','Medical Center assets allow for the salvage and repair of damaged assets. Once between turns, if a Special Forces or Military Unit asset on the world is destroyed, the faction may immediately pay half its purchase cost to restore it with one hit point. Any Repair Asset action taken on that world costs one less FacCred for Special Forces and Military Units.','Wealth')
,('Bank','8','None','None',14,'None','None','False','False','False','False','Bank assets allow for the protection of wealth. Once per turn, the faction can ignore one cost or FacCred loss imposed by another faction, regardless of where it is levied. Multiple banks allow multiple losses to be ignored.','Wealth')
,('Marketers','8','CvW, 1d6','None',15,'CvW','1d6','False','False','False','False','Marketers can be deployed to confuse enemy factions into untimely investments and paralyze them with incompatible hardware and software. As an action, the marketers may test Cunning vs. Wealth against a rival faction’s asset. If successful, the target faction must immediately pay half the asset’s purchase cost, rounded down, or have it become disabled and useless until they do pay.','Wealth')
,('Pretech Researchers','6','None','None',16,'None','None','False','False','False','False','Pretech Researchers are a highly versatile team of research and design specialists capable of bringing the most backward planet up to pretech self-sufficiency... as long as they’re adequately funded. Any world with Pretech Researchers on it is treated as tech level 5 for the purpose of buying Cunning and Wealth assets. Pretech researchers have a maintenance cost of 1 FacCred per turn','Wealth')
,('Blockade Runners','6','None','2d4',17,'None','None','False','False','False','False','Blockade Runners are starship captains that excel at transporting goods through unfriendly lines. As an action, a blockade runner can transfer itself or any one Military Unit or Special Forces to a world within three hexes for a cost of two FacCreds. Blockade Runners can even move units that would otherwise require planetary government permission to be imported into a world.','Wealth')
,('Venture Capital','10','WvW, 2d6','1d6',18,'WvW','2d6','False','False','False','False','Venture Capital grows assets out of seemingly nowhere, harvesting the best of entrepreneurship for the faction’s benefi t. As an action, venture capital can be tapped. 1d8 is rolled; on a 1, the asset is destroyed, while on a 2-3 one FacCred is gained, 4-7 yields two FacCreds and 8 grants three FacCreds.','Wealth')
,('R&D Department','15','None','None',19,'None','None','False','False','False','False','R&D Departments allow the smooth extension of wealth-creation and industrial principles to the farthest reaches of the faction’s operations. A faction with an R&D  Department may treat all planets as having tech level 4 for purposes of purchasing Wealth assets.','Wealth')
;
INSERT INTO assets ("Name",hp,"Attack","Counterattack","Id","AttackStats","AttackDice","DefenderReroll","AttackerReroll","AttackerExtraDice","DefenderExtraDice","Description","Type") VALUES 
('Commodities Broker','10','WvW, 2d8','1d8',20,'WvW','2d8','False','False','False','False','Commodities Brokers can substantially lessen the cost of large-scale investments by timing materials purchases to coincide with gluts in the market. As an action, the owner of a commodities broker can roll 1d8; that many FacCreds are subtracted from the cost of their next asset purchase, down to a minimum of half normal price, rounded down','Wealth')
,('Pretech Manufactory','16','None','None',21,'None','None','False','False','False','False','Pretech Manufactories are rare, precious examples of functioning pretech industrial facilities, retrofitted to work without the benefit of specialized psychic disciplines. As an action, the owning faction can roll 1d8 for a Pretech Manufactory, and gain half that many FacCreds, rounded up.','Wealth')
,('Hostile Takeover','10','WvW, 2d10','2d8',22,'WvW','2d10','False','False','False','False','Hostile Takeover assets allow a faction to seize control of damaged and poorly-controlled assets. If a Hostile Takeover does enough damage to destroy a faction, the target is instead reduced to 1 hit point and acquired by the Hostile Takeover’s owning faction.','Wealth')
,('Transit Web','5','CvC, 1d12','None',23,'CvC','1d12','False','False','False','False','Transit Web facilities allow almost eff ortless relocation of all assets. For one FacCred, any number of non-starship Cunning or Wealth assets may be moved between any two worlds within three hexes of the Transit Web. Th is may be done freely on the owner’s turn so long as the transit fee can be paid and does not require the use of an action.','Wealth')
,('Scavenger Fleet','20','WvW, 2d10+4','2d10',24,'WvW','2d10+4','False','False','False','False','Scavenger Fleets can, very rarely, be persuaded to throw in with particular factions. These rag-tag armadas bring enormous technical and mercantile resources to their patrons, along with a facility with heavy guns. As an action, a Scavenger Fleet can be moved to any world within three hexes. Scavenger fleets cost 2 FacCreds a turn in maintenance','Wealth')
,('Smugglers','4','CvW, 1d4','None',25,'CvW','1d4','False','False','False','False','Smugglers are skilled in extracting personnel. For one FacCred, the smugglers can transport itself and/or any one Special Forces unit to a planet up to two hexes away.','Cunning')
,('Informers','3','CvC, Special','None',26,'CvC','Special','False','False','False','False','Informers lace a planet’s underworld, watchful for intruders. They can choose to Attack any faction, and need not specify a particular asset to target. On a successful Cunning vs. Cunning attack, all Stealthed assets on the planet belonging to that faction are revealed. Informers can target a faction even if none of their assets are visible on a world; at worst, they simply learn that there are no stealthed assets there.','Cunning')
,('False Front','2','None','None',27,'None','None','False','False','False','False','False Front resources allow a faction to preserve more valuable resources. If another asset on the planet suffers enough damage to destroy it, the faction can sacrifice the false front instead to nullify the killing blow.','Cunning')
,('Base Of Influence','1','None','None',28,'None','None','False','False','False','False','Base of Influence assets are special, and are required for purchasing or upgrading units on a particular world. Any damage done to a Base of Influence is also done to a faction’s hit points. The cost of a Base of Influence equals its maximum hit points, which can be any number up to the total maximum hit points of its owning faction. A faction’s bases of influence don’t count against their maximum assets. A Base of Influence can only be purchased with the “Expand Influence” action, and not with a normal “Buy Asset” action','Special')
,('Lobbyists','4','CvC, Special','None',29,'CvC','Special','False','False','False','False','Lobbyists can be used to block the governmental permission that is sometimes required to buy an asset or transport it into a system. When a rival faction gains permission to do so, the Lobbyists can make an immediate Cunning vs. Cunning test against the faction; if successful, the permission is withdrawn and cannot be attempted again until the next turn.','Cunning')
;
INSERT INTO assets ("Name",hp,"Attack","Counterattack","Id","AttackStats","AttackDice","DefenderReroll","AttackerReroll","AttackerExtraDice","DefenderExtraDice","Description","Type") VALUES 
('Saboteurs','6','CvC, 2d4','None',30,'CvC','2d4','False','False','False','False','Saboteurs are trained in launching strikes against crucial enemy operations. An asset attacked by saboteurs cannot use any Action ability until the start of the attacking faction’s next turn. This lock applies whether or not the saboteurs’ attack was successful.','Cunning')
,('Blackmail','4','CvC, 1d6+1','None',31,'CvC','1d6+1','False','False','False','False','Blackmail can be used to selectively degrade the effectiveness of an enemy asset. Any attempt to attack or defend against Blackmail loses any bonus dice earned by tags.','Cunning')
,('Seductress','4','CvC, Special','None',32,'CvC','Special','False','False','False','False','Seductresses and their male equivalents subvert the leadership of enemy assets, revealing hidden plans. As an action, a Seductress can travel to any world within one hex. As an attack, a Seductress does no damage, but an asset that has been successfully attacked immediately reveals any other Stealthed assets on the planet. Only Special Forces units can attack a Seductress.','Cunning')
,('Cyberninjas','4','CvC, 2d6','None',33,'CvC','2d6','False','False','False','False','Cyberninjas are outfitted with the latest in personal stealth cyberware,all designed to avoid all but the most careful scans and inspections.','Cunning')
,('Stealth','0','None','None',34,'None','None','False','False','False','False','Stealth is not an asset, per se, but a special quality that can be purchased for another Special Forces asset on the planet. An asset that has been Stealthed cannot be  detected or attacked by other factions. If the unit normally requires the permission of a planetary government to be moved onto a planet, that permission may be foregone. An asset loses its Stealth if it is used to attack or defend.','Cunning')
,('Covert Shipping','4','None','None',35,'None','None','False','False','False','False','Quiet interstellar asset transport. Any one Special Forces unit can be moved between any worlds within three hexes of the Covert Shipping at the cost of one FacCred.','Cunning')
,('Party Machine','10','CvC, 2d6','1d6',36,'CvC','2d6','False','False','False','False','Party Machines are political blocks that control particular cities or regions - blocks that are firmly in control of the faction. Each turn, a Party Machine provides 1 FacCred to its owning faction.','Cunning')
,('Vanguard Cadres','12','CvC, 1d6','1d6',37,'CvC','1d6','False','False','False','False','Vanguard Cadres are those followers of the movement inspired sufficiently to take up arms and fight on behalf of their leadership.','Cunning')
,('Tripwire Cells','8','None','1d4',38,'None','None','False','False','False','False','Tripwire Cells of observers are intended to alert to the arrival of stealthed units. Whenever a stealthed asset lands or is purchased on a planet with tripwire cells, the Cells make an immediate Cunning vs. Cunning attack against the owning faction. If successful, the asset loses its stealth.','Cunning')
,('Seditionists','8','None','None',39,'None','None','False','False','False','False','Seditionists baffle and confuse their targets, sapping their will to obey.  For 1d4 FacCreds, the Seditionists can attach themselves to an enemy asset.  Until they attach to a different asset or no longer share the same planet, the affected asset cannot perform an attack action.','Cunning')
;
INSERT INTO assets ("Name",hp,"Attack","Counterattack","Id","AttackStats","AttackDice","DefenderReroll","AttackerReroll","AttackerExtraDice","DefenderExtraDice","Description","Type") VALUES 
('Organization Moles','8','CvC, 2d6','None',40,'CvC','2d6','False','False','False','False','Organization Moles can subvert and confuse enemy assets, striking to damage their cohesion.','Cunning')
,('Cracked Comms','6','None','Special',41,'None','None','False','False','False','False','Cracked Comms indicate a cryptographic asset for the interception and deciphering of enemy communications. Friendly fire can be induced with the right interference. If the Cracked Comms succeeds in defending against an attack, it can immediately cause the attacking asset to attack itself for normal damage and counterattack consequences.','Cunning')
,('Boltholes','6','None','2d6',42,'None','None','False','False','False','False','Boltholes are equipped with a number of postech innovations to make cleaning them out a costly and dangerous pursuit. If a faction Special Forces or Military Unit asset on the same planet as the Boltholes suffers damage sufficient to destroy it, it is instead set at 0 HP and rendered untouchable and unusable until it is repaired to full strength. If the Boltholes are destroyed before this happens, the asset is destroyed with them.','Cunning')
,('Transport Lockdown','10','CvC, Special','None',43,'CvC','Special','False','False','False','False','Transport Lockdown techniques involve selective pressure on local routing and shipping companies. On a successful Cunning vs. Cunning attack against a rival faction, the rival faction cannot transport assets onto that planet without spending 1d4 FacCreds and waiting one turn.','Cunning')
,('Covert Transit Net','15','None','None',44,'None','None','False','False','False','False','Facilities web an area of space with a network of smugglers and gray-market freighter captains. As an action, any Special Forces assets can be moved between any worlds within three hexes of the Covert Transit Net','Cunning')
,('Demagogue','10','CvC, 2d8','1d8',45,'CvC','2d8','False','False','False','False','Demagogues are popular leaders of a particular faith or ideology that can be relied upon to point their followers in the direction of maximum utility for the faction.','Cunning')
,('Popular Movement','16','CvC, 2d6','1d6',46,'CvC','2d6','False','False','False','False','Popular Movements represent a planet-wide surge of enthusiasm for a cause controlled by the faction. This support pervades all levels of government, and the government always grants any asset purchase or movement requests made by the faction.','Cunning')
,('Book Of Secrets','10','None','2d8',47,'None','None','False','False','False','False','Book of Secrets assets represent exhaustively catalogued psychometric records on important and influential local figures, allowing uncanny accuracy in predicting their actions. Once per turn, a Book of Secrets allows the faction to reroll one die for an action taken on that world or force an enemy faction to reroll one die. This reroll can only be forced once per turn, no matter how many Books of Secrets are owned.','Cunning')
,('Treachery','5','CvC, Special','None',48,'CvC','1d1-1','False','False','False','False','Treachery can attack an enemy asset. On a successful attack, the Treachery asset is lost, 5 FacCreds are gained, and the targeted asset switches sides to join the traitor’s faction, even if the faction does not  otherwise have the attributes necessary to purchase it.','Cunning')
,('Panopticon Matrix','20','None','1d6',49,'None','None','False','False','False','False','Panopticon Matrix facilities weave braked-AI intelligence analysts into a web of observation capable of detecting the slightest evidence of intruders on a world. Every rival Stealthed asset on the planet must succeed in a Cunning vs. Cunning test at the beginning of every turn or lose their Stealth. The owner also gains an additional die on all Cunning attacks and defenses on that planet.','Cunning')
;
INSERT INTO assets ("Name",hp,"Attack","Counterattack","Id","AttackStats","AttackDice","DefenderReroll","AttackerReroll","AttackerExtraDice","DefenderExtraDice","Description","Type") VALUES 
('Security Personnel','3','FvF, 1d3+1','1d4',50,'FvF','1d3+1','False','False','False','False','Security Personnel are standard civilian guards or policemen, usually equipped with nonlethal weaponry or personal sidearms.','Force')
,('Hitmen','1','FvC, 1d6','None',51,'FvC','1d6','False','False','False','False','Hitmen are crudely-equipped thugs and assassins with minimal training that have been aimed at a rival faction’s leadership.','Force')
,('Militia Unit','4','FvF, 1d6','1d4+1',52,'FvF','1d6','False','False','False','False','Militia Units are groups of lightly-equipped irregular troops with rudimentary military training but no heavy support.','Force')
,('Base Of Influence','1','None','None',53,'None','None','False','False','False','False','Base of Influence assets are special, and are required for purchasing or upgrading units on a particular world. Any damage done to a Base of Influence is also done to a faction’s hit points. The cost of a Base of Influence equals its maximum hit points, which can be any number up to the total maximum hit points of its owning faction. A faction’s bases of influence don’t count against their maximum assets. A Base of Influence can only be purchased with the “Expand Influence” action, and not with a normal “Buy Asset” action','Special')
,('Heavy Drop Assets','6','None','None',54,'None','None','False','False','False','False','Heavy Drop Assets allow for the transport of resources from one world to another. As an action, any one non-Starship asset -including this one- may be moved to any world within one hex for one FacCred.','Force')
,('Elite Skirmishers','5','FvF, 2d4','1d4+1',55,'FvF','2d4','False','False','False','False','Elite Skirmishers are lightly-equipped troops trained for guerilla warfare and quick raids.','Force')
,('Hardened Personnel','4','None','1d4+1',56,'None','None','False','False','False','False','Hardened Personnel assets consist of employees and support staff of the faction that have been trained in defensive fighting and equipped with supply caches and hardened fallback positions.','Force')
,('Guerilla Populace','6','FvC, 1d4+1','None',57,'FvC','1d4+1','False','False','False','False','Guerilla Populace assets reflect popular support among the locals and a cadre of men and women willing to fight as partisans.','Force')
,('Zealots','4','FvF, 2d6','2d6',58,'FvF','2d6','False','False','False','False','Zealots are members of the faction so utterly dedicated that they are willing to commit suicide attacks. Zealots take 1d4 damage every time they launch a successful attack or perform a counterattack.','Force')
,('Cunning Trap','2','None','1d6+3',59,'None','None','False','False','False','False','Cunning Traps involve all the myriad stratagems of war, from induced landslides to spreading local diseases.','Force')
;
INSERT INTO assets ("Name",hp,"Attack","Counterattack","Id","AttackStats","AttackDice","DefenderReroll","AttackerReroll","AttackerExtraDice","DefenderExtraDice","Description","Type") VALUES 
('Counterintel Unit','4','CvC, 1d4+1','1d6',60,'CvC','1d4+1','False','False','False','False','Counterintel Units specialize in code breaking, internal security, and monitoring duties. They can crack open enemy plots long before they have time to come to fruition.','Force')
,('Beachhead Landers','10','None','None',61,'None','None','False','False','False','False','Beachhead Landers are a collection of short-range, high-capacity spike drive ships capable of moving large numbers of troops. As an action, the asset may move any number of assets on the planet including itself- to any world within one hex at a cost of one FacCred per asset moved.','Force')
,('Extended Theater','10','None','None',62,'None','None','False','False','False','False','Extended Theater facilities allow for transporting assets long distances. As an action, any one non-Starship asset - including itself - can be moved between any two worlds within two hexes of the extended theater, at a cost of 1 FacCred.','Force')
,('Strike Fleet','8','FvF, 2d6','1d8',63,'FvF','2d6','False','False','False','False','Strike Fleets are composed of frigate or cruiser-class vessels equipped with space-to-ground weaponry and sophisticated defenses against light planetary weaponry. As an action, they can move to any world within one hex of their current location.','Force')
,('Postech Infantry','12','FvF, 1d8','1d8',64,'FvF','1d8','False','False','False','False','Postech Infantry are the backbone of most planetary armies. These well-trained soldiers are usually equipped with mag weaponry and combat field uniforms, and have heavy support units attached.','Force')
,('Blockade Fleet','8','FvW, 1d6','None',65,'FvW','1d6','False','False','False','False','Blockade Fleets include a ragtag lot of corsairs, pirates, privateers, and other deniable assets. When they successfully hit an enemy faction asset, they steal 1d4 FacCreds from the target faction as well. This theft can occur to a faction only once per turn, no matter how many blockade fleets attack. As an action, this asset may also move itself to a world within one hex.','Force')
,('Pretech Logistics','6','None','None',66,'None','None','False','False','False','False','Pretech Logistics assets represent caches, smugglers, or internal research and salvage programs. As an action, a pretech logistics asset allows the owner to buy one Force asset on that world that requires up to tech level 5 to purchase. This asset costs half again as many FacCreds as usual, rounded up. Only one asset can be purchased per turn.','Force')
,('Psychic Assassins','4','CvC, 2d6+2','None',67,'CvC','2d6+2','False','False','False','False','Psychic Assassins are combat-trained psychics equipped with advanced pretech stealth gear and psitech weaponry. Psychic assassins automatically start Stealthed when purchased.','Force')
,('Pretech Infantry','16','FvF, 2d8','2d8+2',68,'FvF','2d8','False','False','False','False','Pretech Infantry are the cream of the stellar ground forces, elite troops kitted out in the best pretech weaponry and armor available, with sophisticated heavy support weaponry integral to the unit.','Force')
,('Planetary Defenses','20','None','2d6+6',69,'None','None','False','False','False','False','Planetary Defenses are massive mag cannons and seeker missile arrays designed to defend against starship bombardments. Planetary Defenses can only defend against attacks by Starship-type assets.','Force')
;
INSERT INTO assets ("Name",hp,"Attack","Counterattack","Id","AttackStats","AttackDice","DefenderReroll","AttackerReroll","AttackerExtraDice","DefenderExtraDice","Description","Type") VALUES 
('Gravtank Formation','14','FvF, 2d10+4','1d10',70,'FvF','2d10+4','False','False','False','False','Gravtank Formations are composed of advanced pretech gravtank units that are capable of covering almost any terrain and cracking the toughest defensive positions.','Force')
,('Deep Strike Landers','10','None','None',71,'None','None','False','False','False','False','Deep Strike Landers are advanced pretech transport ships capable of moving an asset long distances. As an action, any one non-Starship asset- including itself- can be moved between any two worlds within three hexes of the deep strike landers, at a cost of 2 FacCreds. This movement can be done even if the local planetary government objects, albeit doing so is usually an act of open war.','Force')
,('Integral Protocols','10','None','2d8+2',72,'None','None','False','False','False','False','Integral Protocols are a complex web of braked-AI supported sensors and redundant security checks used to defeat attempts to infiltrate an area. They can defend only against attacks versus Cunning, but they add an additional die to the defender’s roll.','Force')
,('Space Marines','16','FvF, 2d8+2','2d8',73,'FvF','2d8+2','False','False','False','False','Space Marines are heavily-armored specialist troops trained for ship boarding actions and opposed landings. As an action, they can move to any world within one hex of their current location, whether or not the planetary government permits it.','Force')
,('Capital Fleet','30','FvF, 3d10+4','3d8',74,'FvF','3d10+4','False','False','False','False','Capital Fleets are the pride of an empire, a collection of massive pretech warships without peer in most sectors. Capital fleets are expensive to keep flying, and cost an additional 2 FacCreds of maintenance each turn. As an action, they may move to any world within three hexes of their current location. Planetary government permission is required to raise a capital fleet, but not to move one into a system.','Force')
;