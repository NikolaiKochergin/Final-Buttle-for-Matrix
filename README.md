# Final-Buttle-for-Matrix

Project to complete Matrix final buttle

UnitStatMachine:
Отвечает за переключение состояний юнита через вызовы методов Ser<>State
В самих State вызываются методы юнита необходимые для данного состояния в Enter при входе в состояние Exite при выходе из состояния 
в Update методы при нахождении в состоянии, если ноебходмо.

Unit:
Этот класс можно было бы разбить на подклассы, но я думаю тут это не нужно.

Для поиска цели классу необходимо передать список возможных целей (в данном случае класса Unit). У меня это реализовано через вспомогательный класс Team.
В инспекторе соответственно нужно закинуть список команды противников.

Примечание по проекту:
<В проекте соответстветвенно нужно Helper'ам передать список противников из босс файта. Неочень понятно мне где он хранится, увидел _theOneEnemies и _nerdEnemies в классе Enemies,
судя по всему один из них. А противникам передать список Helper'ов.>

  Take(damage) при получении фатального урона сразу выставляется флаг IsAlive в false чтобы кокретного юнита нельзя было выставить в качестве цели. Предполагаю, что в конце
  анимации смерти gameObject юнита должен выключаться, по этому в конце этой анимации нужно выставить триггер и назначить на него метод HandleDieAnimation.
  
  HitTarget() тут думаю все понятно.
  
  FindTarget() тут соответственно ищем ближаюшую цель из списка целей с флагом IsAlive == true. Если все цели мертвы возвращаем null.
  
  SetTarget() если получили null, значит все противники мертвы, инвокаем Waiting -> машина состояний переводит юнита в состояние ожидания. Если цель есть подписываемся
  на смерть противника.
  
  Остальное в принципе должно быть понятно, хотя я может и выше написал очевидные вещи.
  
ВАЖНО:
В иерархии объекта в сцене NavMeshAgent и NavMeshObstacle не должны быть на одном объекте, чтобы небыло глюков при перемещении. Я например, повесил NavMeshObstacle
на саму "модельку" ниже в иерархии.
Т.к. мы ильзуем кинематики для того чтобы модельки юнитов не накладывались друг на друга в настройках NavMeshAgent -> Obstacle Avoidance необходимо выставить Radius
на необходимую величину, играет роль коллайдера в каком то смысле. При этом в настройка юнита HitDistance должен быть больше, а то юниты не смогут подойти друг к другу,
чтобы ударить.
  
