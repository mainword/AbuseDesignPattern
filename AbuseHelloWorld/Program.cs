using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbuseHelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World");
            AbstractFactory factory = FactoryMakerSingleton.getInstance().getFactory();

            Subject subject = factory.createSubject();
            Observer observer = factory.createObserver();
            subject.attach(observer);

            Command command = factory.createCommand(subject);
            command.execute();

            Console.ReadLine();
        }
    }

    //Declare Subject and Observer interface
    public interface Subject
    {
        void attach(Observer observer);
        void detach(Observer observer);
        void notifyObservers();
    }

    public interface Observer
    {
        void update(Subject subject);
    }

    // implement Subject and Observer interface
    public class HelloWorldSubject : Subject 
    {
        private List<Observer> observers;
        private String str;
     
        public HelloWorldSubject() : base() {
            this.observers = new List<Observer>();
        }
 
        public void attach(Observer observer) {
            this.observers.Add(observer);
        }
 
        public void detach(Observer observer) {
            this.observers.Remove(observer);
        }
 
        public void notifyObservers() {
            foreach(var observer in this.observers){
                observer.update(this);
            }
        }
     
        public String getStr() {
            return this.str;
        }
 
        public void setStr(String str) {
            this.str = str;
            this.notifyObservers();
        }
    }
 
    public class HelloWorldObserver : Observer 
    {
        public void update(Subject subject) {
            HelloWorldSubject sub = (HelloWorldSubject)subject;
           Console.WriteLine(sub.getStr());
        }
    }

    //3. Add Command 
    public interface Command
    {
        void execute();
    }

    public class HelloWorldCommand : Command
    {
        private HelloWorldSubject subject;

        public HelloWorldCommand(Subject subject)
            : base()
        {
            this.subject = (HelloWorldSubject)subject;
        }

        public void execute()
        {
            subject.setStr("hello world");
        }
    }

    //4. Abstract Factory
    public interface AbstractFactory
    {
        Subject createSubject();
        Observer createObserver();
        Command createCommand(Subject subject);
    }

    public class HelloWorldFactory : AbstractFactory
    {
        public Subject createSubject()
        {
            return new HelloWorldSubject();
        }

        public Observer createObserver()
        {
            return new HelloWorldObserver();
        }

        public Command createCommand(Subject subject)
        {
            return new HelloWorldCommand(subject);
        }
    }

    //5.Singleton
    public class FactoryMakerSingleton
    {
        private static FactoryMakerSingleton instance = null;
        private static object syncRoot = new Object();
        private AbstractFactory factory;

        private FactoryMakerSingleton()
        {
            factory = new HelloWorldFactory();
        }

        public static FactoryMakerSingleton getInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new FactoryMakerSingleton();
                }
            }
            return instance;
        }

        public AbstractFactory getFactory()
        {
            return factory;
        }
    }




}
