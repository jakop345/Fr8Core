﻿module dockyard.tests.utils.fixtures {

    export class ProcessBuilder {
        public static newProcessTemplate = <interfaces.IRouteVM> {
            id: 1,
            name: "MockProcessTemplate",
            description: "MockProcessTemplate",
            routeState: 1,
            subscribedDocuSignTemplates: [],
            externalEventSubscription: [],
            startingSubrouteId: 1
        };

        public static processBuilderState = new model.ProcessBuilderState();

        public static updatedProcessTemplate = <interfaces.IRouteVM> {
            'name': 'Updated',
            'description': 'Description',
            'routeState': 1,
            'subscribedDocuSignTemplates': ['58521204-58af-4e65-8a77-4f4b51fef626']
        }

        public static fullProcessTemplate = <interfaces.IRouteVM> {
            'name': 'Updated',
            'description': 'Description',
            'routeState': 1,
            'subscribedDocuSignTemplates': ['58521204-58af-4e65-8a77-4f4b51fef626'],
            subroutes: [
                <model.SubrouteDTO>{
                    id: 1,
                    isTempId: false,
                    name: 'Processnode Template 1',
                    actions: [
                        <model.ActionDTO> {
                            id: 1,
                            name: 'Action 1',
                            activityTemplate: {
                                id: 1
                            },
                            parentRouteNodeId: 1
                        },
                        <model.ActionDTO>{
                            id: 2,
                            name: 'Action 2',
                            activityTemplate: {
                                id: 1
                            },
                            parentRouteNodeId: 1
                        }
                    ]
                }]
        }
    }
}
