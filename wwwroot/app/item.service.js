"use strict";var __decorate=this&&this.__decorate||function(decorators,target,key,desc){var d,c=arguments.length,r=c<3?target:null===desc?desc=Object.getOwnPropertyDescriptor(target,key):desc;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)r=Reflect.decorate(decorators,target,key,desc);else for(var i=decorators.length-1;i>=0;i--)(d=decorators[i])&&(r=(c<3?d(r):c>3?d(target,key,r):d(target,key))||r);return c>3&&r&&Object.defineProperty(target,key,r),r},__metadata=this&&this.__metadata||function(k,v){if("object"==typeof Reflect&&"function"==typeof Reflect.metadata)return Reflect.metadata(k,v)};Object.defineProperty(exports,"__esModule",{value:!0});var core_1=require("@angular/core"),http_1=require("@angular/http"),Observable_1=require("rxjs/Observable");require("rxjs/Rx");var ItemService=function(){function ItemService(http){this.http=http,this.baseUrl="api/items/"}return ItemService.prototype.handleError=function(error){return console.error(error),Observable_1.Observable.throw(error.json().error||"Server error")},ItemService.prototype.getLatest=function(num){var url=this.baseUrl+"GetLatest/";return null!=num&&(url+=num),this.http.get(url).map(function(response){return response.json()}).catch(this.handleError)},ItemService.prototype.getMostViewed=function(num){var url=this.baseUrl+"GetMostViewed/";return null!=num&&(url+=num),this.http.get(url).map(function(response){return response.json()}).catch(this.handleError)},ItemService.prototype.getRandom=function(num){var url=this.baseUrl+"GetRandom/";return null!=num&&(url+=num),this.http.get(url).map(function(response){return response.json()}).catch(this.handleError)},ItemService.prototype.get=function(id){if(null==id)throw new Error("Id required");var url=this.baseUrl+id;return this.http.get(url).map(function(response){return response.json()}).catch(this.handleError)},ItemService}();ItemService=__decorate([core_1.Injectable(),__metadata("design:paramtypes",[http_1.Http])],ItemService),exports.ItemService=ItemService;
//# sourceMappingURL=item.service.js.map
