"use strict";var __decorate=this&&this.__decorate||function(decorators,target,key,desc){var d,c=arguments.length,r=c<3?target:null===desc?desc=Object.getOwnPropertyDescriptor(target,key):desc;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)r=Reflect.decorate(decorators,target,key,desc);else for(var i=decorators.length-1;i>=0;i--)(d=decorators[i])&&(r=(c<3?d(r):c>3?d(target,key,r):d(target,key))||r);return c>3&&r&&Object.defineProperty(target,key,r),r},__metadata=this&&this.__metadata||function(k,v){if("object"==typeof Reflect&&"function"==typeof Reflect.metadata)return Reflect.metadata(k,v)};Object.defineProperty(exports,"__esModule",{value:!0});var core_1=require("@angular/core"),router_1=require("@angular/router"),item_service_1=require("./item.service"),ItemListComponent=function(){function ItemListComponent(itemService,router){this.itemService=itemService,this.router=router}return ItemListComponent.prototype.ngOnInit=function(){var _this=this;console.log("ItemListComponent instantiated with the following type: "+this.class);var s=null;switch(this.class){case"latest":default:this.title="Latest Items",s=this.itemService.getLatest();break;case"most-viewed":this.title="Most Viewed Items",s=this.itemService.getMostViewed();break;case"random":this.title="Random Items",s=this.itemService.getRandom()}s.subscribe(function(items){return _this.items=items},function(error){return _this.errorMessage=error})},ItemListComponent.prototype.onSelect=function(item){this.selectedItem=item,console.log("Item "+this.selectedItem.Id+" has been clicked: loading ItemDetailComponent..."),this.router.navigate(["item",this.selectedItem.Id])},ItemListComponent}();__decorate([core_1.Input(),__metadata("design:type",String)],ItemListComponent.prototype,"class",void 0),ItemListComponent=__decorate([core_1.Component({selector:"item-list",template:'\n        <h2>{{title}}</h2>\n        <ul class="items">\n            <li *ngFor="let item of items" \n                [class.selected]="item === selectedItem"\n                (click)="onSelect(item)">\n                <span>{{item.Title}}</span>\n            </li>\n        </ul>\n    ',styles:["\n        ul.items li { \n            cursor: pointer;\n        }\n        ul.items li.selected { \n            background-color: #dddddd; \n        }\n    "]}),__metadata("design:paramtypes",[item_service_1.ItemService,router_1.Router])],ItemListComponent),exports.ItemListComponent=ItemListComponent;
//# sourceMappingURL=item-list.component.js.map
