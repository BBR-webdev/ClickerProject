using UnityEngine;
using UnityEngine.UI;

public class IdleGameBehaviour : MonoBehaviour
{

    // This script carries all mechanical behaviours for the agent idle game
    // First "real" Unity Project, developed by Björn Breski (BBRWEBDEV)
 
   
    // Initialize objects to address UI
    
    // Resources
    public Text agentText; // displays # of available agents -> Base resource
    public Text intelText; // displays # of available intel -> 2nd level resource
    public Text cashText; // displays # of available cash -> 3rd level resource, necessary for most upgrades

    // Generators
    // Each generator has real and a fake version. Fake version is displayed when not enough resource to buy
    public GameObject RecruiterButton;
    public Text recruiterButtonText;
    public GameObject FakeRecruiterButton;
    public Text fakeRecruiterButtonText;

    public GameObject BrokerButton;
    public Text BrokerButtonText;
    public GameObject FakeBrokerButton;
    public Text FakeBrokerButtonText;

    public GameObject SpecialAgentButton;
    public Text SpecialAgentText;
    public GameObject FakeSpecialAgentButton;
    public Text FakeSpecialAgentText;

    public GameObject RecruitmentTrainingButton;
    public Text RecruitmentTrainingText;
    public GameObject FakeRecruitmentTrainingButton;
    public Text FakeRecruitmentTrainingText;
    
    // Additional UI Elements
    public Text infoBoxText; // info box, currently only used for errors -> later mouse over UI elements displays tooltip in infobox?
    public Text agentsPerSecondText;
    public Text intelPerSecondText;
    public Text cashPerSecondText;

    // Variables
    public double agents;
    public double intel;
    public double cash;

    public double agentsPerSecond;
    public double intelPerSecond;
    public double cashPerSecond;

    public double lowUpgradeFactor = 1.07;
    public double intelSellingPrice;
    public double agentToIntelRate;



    public double recruiterUpgradePrice;
    public double brokerUpgradePrice;
    public double specialAgentUpgradePrice;
    public double recruitmentTrainingPrice;

    public void Start()
    {
        // set startgame values to variables
        agents = 0;
        intel = 0;
        cash = 0;
        agentsPerSecond = 0;
        intelPerSecond = 0;
        cashPerSecond = 0;
        recruiterUpgradePrice = 5000;
        intelSellingPrice = 500;
        specialAgentUpgradePrice = 25000;
        recruitmentTrainingPrice = 150000;
        brokerUpgradePrice = 12500;
        agentToIntelRate = 1;
        
    }

    public void Update()
    {
        
        agentText.text = "Agents: " + agents.ToString("F0");
        intelText.text = "Intel: " + intel.ToString("F0");
        cashText.text = "Cash: $" + cash.ToString("F0");

        agentsPerSecondText.text = "Recruiting "+ agentsPerSecond.ToString("F0") + " agents/sec";
        intelPerSecondText.text = "Gathering " + intelPerSecond.ToString("F0") + " intel/sec";
        cashPerSecondText.text = "Earning $" + cashPerSecond.ToString("F2") + "/sec";

        // Updates the # of resources according to resource per sec values
        agents += agentsPerSecond * Time.deltaTime;
        // FIX HERE
        intel += intelPerSecond * Time.deltaTime;
        if (intel < 1)
        {
            Debug.Log("Not enough intel");
        }
        else
        {
            cash += cashPerSecond * Time.deltaTime;

        }

        // Keeps prices on the button labels up to date
        recruiterButtonText.text = "Hire Recruiter - $" + recruiterUpgradePrice.ToString("F0");
        fakeRecruiterButtonText.text = "Hire Recruiter - $" + recruiterUpgradePrice.ToString("F0");
        FakeBrokerButtonText.text = "Hire Broker - $" + brokerUpgradePrice.ToString("F0");
        BrokerButtonText.text = "Hire Broker - $" + brokerUpgradePrice.ToString("F0");
        FakeSpecialAgentText.text = "Train Special Agent - $" + specialAgentUpgradePrice;
        SpecialAgentText.text = "Train Special Agent - $" + specialAgentUpgradePrice;
        FakeRecruitmentTrainingText.text = "Train Recruiters - $" + recruitmentTrainingPrice;
        RecruitmentTrainingText.text = "Train Recruiters - $" + recruitmentTrainingPrice;


        // if-else statements that toggle button visibility depending on # of resources
        if (cash >= recruiterUpgradePrice)
        {
            RecruiterButton.SetActive(true);
            FakeRecruiterButton.SetActive(false);
        }

        else
        {
            RecruiterButton.SetActive(false);
            FakeRecruiterButton.SetActive(true);
        }

        if (cash >= brokerUpgradePrice)
        {
            BrokerButton.SetActive(true);
            FakeBrokerButton.SetActive(false);
        }

        else
        {
            BrokerButton.SetActive(false);
            FakeBrokerButton.SetActive(true);
        }

        if (cash >= specialAgentUpgradePrice)
        {
            SpecialAgentButton.SetActive(true);
            FakeSpecialAgentButton.SetActive(false);
        }

        else
        {
            SpecialAgentButton.SetActive(false);
            FakeSpecialAgentButton.SetActive(true);
        }

        if (cash >= recruitmentTrainingPrice)
        {
            RecruitmentTrainingButton.SetActive(true);
            FakeRecruitmentTrainingButton.SetActive(false);
        }

        else
        {
            RecruitmentTrainingButton.SetActive(false);
            FakeRecruitmentTrainingButton.SetActive(true);
        }
    }

    // Behaviour for the Gather Agents button
    public void MainButtonClick()
    {
        agents += 1;
    }

    // Behaviour for the Gather Intel button
    public void GatherIntelClick()
    {

        if (agents < 1)
        {
            infoBoxText.text = "Not enough agents!";
            
        }
        else
        {
            
            agents -= 1;
            intel += agentToIntelRate;

        }
    }

    // Behaviour for the Sell Intel Button
    public void SellIntel()
    {
        if (intel < 1)
        {
            infoBoxText.text = "Not enough intel!";
        }
        else
        { 
            intel -= 1;
            cash += intelSellingPrice;
        }
    }

    // Methods for the generators
    public void HireRecruiter()
    {
        if (cash >= recruiterUpgradePrice)
        {
            cash -= recruiterUpgradePrice;
            agentsPerSecond++;
            recruiterUpgradePrice *= lowUpgradeFactor;

           
        }

        else
        {
            infoBoxText.text = "Not enough cash!";
        }
        
    }
    // FIX HERE
    public void HireBroker()
    {
        if (cash >= brokerUpgradePrice)
        {
            cash -= brokerUpgradePrice;
            cashPerSecond += 500;
            intelPerSecond -= 1;
            brokerUpgradePrice *= lowUpgradeFactor;
        }

        else
        {
            infoBoxText.text = "Not enough cash!";
        }
    }

    public void SpecialAgentUpgrade()
    {
        if (cash >= specialAgentUpgradePrice)
        {
            cash -= specialAgentUpgradePrice;
            specialAgentUpgradePrice *= 5;
            agentToIntelRate += 1;
        }

        else
        {
            infoBoxText.text = "Not enough cash!";
        }
    }

    public void RecruitmentTraining()
    {
        if (cash >= recruitmentTrainingPrice)
        {
            cash -= recruitmentTrainingPrice;
            recruitmentTrainingPrice *= 7.3;
            agentsPerSecond *= 2;
        }
    }

   
}
