﻿
module dockyard.tests.utils.fixtures {

    export class FieldDTO {

        public static newRoute = <interfaces.IRouteVM>{
            name: 'Test',
            description: 'Description',
            routeState: 1
        };

        public static filePickerField: model.File = {
            type: 'FilePicker',
            fieldLabel: 'FilePicker Test',
            name: 'FilePickerTest',
            events: [],
            value: null,
            errorMessage: null,
            isFocused: false
        };

        public static textField: model.TextBox = {
            required: true,
            type: 'TextBox',
            fieldLabel: 'test',
            name: 'test',
            events: [],
            errorMessage: null,
            isFocused: false,
            value: 'test'
        };

        public static textBlock: model.TextBlock = new model.TextBlock('<span>teststs</span>', 'well well-lg');

        public static dropDownListBox: model.DropDownList = {
            listItems: [{ key: 'test1', selected: false, value: 'value1', tags: null, availability: model.AvailabilityType.Configuration, sourceCrateLabel: null, sourceCrateManifest: null }, { key: 'test2', selected: false, value: 'value2', tags: null, availability: model.AvailabilityType.Configuration, sourceCrateLabel: null, sourceCrateManifest: null }, { key: 'test3', selected: false, value: 'value3', tags: null, availability: model.AvailabilityType.Configuration, sourceCrateLabel: null, sourceCrateManifest: null }],
            source: {
                manifestType: 'testManifest',
                label: 'testLabel',
                filterByTag: null,
                requestUpstream: false
            },
            type: 'DropDownList',
            fieldLabel: 'DropDownList Test',
            name: 'DropDownList',
            isFocused: false,
            events: [],
            value: 'value3',
            errorMessage: null,
            selectedKey: 'test3'
        };

        public static radioButtonGroupField: model.RadioButtonGroup = {
            groupName: 'SMSNumber_Group',
            radios: [
                {
                    selected: false,
                    name: 'SMSNumberOption',
                    value: 'SMS Number',
                    type: "RadioButtonGroup",
                    fieldLabel: null,
                    events: null,
                    errorMessage: null,
                    isFocused: false,
                    controls: [
                        {
                            name: 'SMS_Number',
                            value: null,
                            fieldLabel: null,
                            type: "TextBox",
                            events: null,
                            errorMessage: null,
                            isFocused: false
                        }
                    ]
                },
                {
                    selected: false,
                    name: 'SMSNumberOption',
                    value: 'A value from Upstream Crate',
                    type: "RadioButtonGroup",
                    fieldLabel: null,
                    events: null,
                    errorMessage: null,
                    isFocused: false,
                    controls: [
                        {
                            name: 'SMS_Number2',
                            value: null,
                            fieldLabel: null,
                            type: "TextBox",
                            events: null,
                            isFocused: false,
                            errorMessage: null,
                        }
                    ]
                }
            ],
            name: '',
            value: null,
            fieldLabel: "For the SMS Number use:",
            type: "RadioButtonGroup",
            errorMessage: null,
            isFocused: false,
            events: null
        };


        public static designTimeField = {
            Key: 'test2',
            Value: 'value'
        };

        public static fieldList = {
            value: JSON.stringify([FieldDTO.designTimeField]),
            field: 'test2'
        };


        public static textSource: model.TextSource = {
            type: "TextSource",
            events: [],
            initialLabel: 'test label',
            value: null,
            textValue: null,
            errorMessage: null,
            isFocused: false,
            source: {
                manifestType: 'testManifest',
                label: 'testLabel',
                filterByTag: null,
                requestUpstream: false
            },
            valueSource: 'test',
            listItems: [{
                key: 'test1',
                selected: false,
                value: 'value1', tags: null, availability: model.AvailabilityType.Configuration, sourceCrateLabel: null, sourceCrateManifest: null
            },
                {
                    key: 'test2',
                    selected: false,
                    value: 'value2', tags: null, availability: model.AvailabilityType.Configuration, sourceCrateLabel: null, sourceCrateManifest: null
                },
                { key: 'test3', selected: false, value: 'value3', tags: null, availability: model.AvailabilityType.Configuration, sourceCrateLabel: null, sourceCrateManifest: null }],
            name: 'test name',
            fieldLabel: 'test label',
            selectedKey: null
        };
    }
} 