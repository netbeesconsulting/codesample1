
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { SearchRequestModel } from '../model/searchRequestModel';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ExtensionService {

  protected MaximumImagesAllowedCount = 5;
  protected baseApiEndPoint = environment.apiEndPoint;
  protected imagePath = environment.imagePath;
  constructor(protected toasterService: ToastrService) { }

  setup(actionurl: string) {
    return this.baseApiEndPoint + actionurl;
  }

  setupReadAll(actionurl: string, searchRequestModel: SearchRequestModel) {
    return this.baseApiEndPoint + actionurl + `?skip=${searchRequestModel.skip}&take=${searchRequestModel.take}&searchTerm=${searchRequestModel.searchTerm}&orderby=${searchRequestModel.orderBy}`;
  }

  success(message: string) {
    this.toasterService.success(message, "Success");
  }

  warning(errorMessage: string) {
    this.toasterService.warning(errorMessage, "Warning");
  }

  renderImage(path: string, images: Array<string>) {
    let renderedImages = new Array<string>();
    images.forEach(element => {
      renderedImages.push(this.imagePath + `${path}/${element}`);
    });
    return renderedImages;
  }
}
