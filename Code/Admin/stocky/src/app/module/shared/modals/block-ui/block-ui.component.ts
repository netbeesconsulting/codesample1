import { Component, OnInit, Input } from '@angular/core';
import { BlockUIService } from 'src/app/module/core/services/block-ui.service';

@Component({
  selector: 'app-block-ui',
  templateUrl: './block-ui.component.html',
  styleUrls: ['./block-ui.component.scss']
})
export class BlockUIComponent implements OnInit {

  @Input() isBlocked = false;
  constructor(blockUIService: BlockUIService) { 
    blockUIService.start$.subscribe(message => { this.isBlocked = message; });
  }

  ngOnInit() {
  }

}
