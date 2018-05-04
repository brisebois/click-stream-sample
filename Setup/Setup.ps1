$RG = 'click-stream-demo'
$LOCATION = 'East US'

$DBNAME = 'click-stream-sample'
$DBLOCATION = 'eastus'

$STRGNAME = 'clickstreamdemostorage'
$FUNCLGNAME = 'cs-load-gen'

$APPSERVICEPLANNAME = 'cs-service-plan'

$APIAPPNAME = 'click-stream-api'

New-AzureRmResourceGroup -Name $RG -Location $LOCATION

New-AzureRmResourceGroupDeployment -ResourceGroupName $RG `
                                   -Name 'click-stream-sample-setup' `
                                   -TemplateFile .\Setup\deploy.json `
                                   -TemplateParameterObject @{
                                        dbName = $DBNAME
                                        dbLocation = $DBLOCATION
                                        dbLocationName = $LOCATION
                                        storageName = $STRGNAME
                                        storageLocation = $LOCATION
                                        loadGenFunctionAppLocation = $LOCATION
                                        loadGenFunctionAppName = $FUNCLGNAME
                                        hostingPlanLocation = $LOCATION
                                        hostingPlanName = $APPSERVICEPLANNAME
                                        hostingPlanWorkerSize = "0"
                                        hostingPlanSku = "Standard"
                                        hostingPlanSkuCode = "S1"
                                        apiAppName = $APIAPPNAME
                                        eventHubNamespaceName = "click-stream-ns"
                                        eventHubName = "click-stream"
                                        eventHubLocation = $LOCATION
                                    }

# Set Cosmos KEY to Environments

(Invoke-AzureRmResourceAction -Action listKeys `
                             -ResourceType "Microsoft.DocumentDb/databaseAccounts" `
                             -ApiVersion "2015-04-08" `
                             -ResourceGroupName $RG `
                             -Name $DBNAME).primaryMasterKey
                             