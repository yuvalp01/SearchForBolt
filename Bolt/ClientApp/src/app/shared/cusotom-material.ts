import { MatButtonModule } from '@angular/material/button';
import { NgModule } from '@angular/core';
import { TextFieldModule } from '@angular/cdk/text-field';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatFormFieldModule } from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatListModule} from '@angular/material/list';


@NgModule({
  imports: [
    MatButtonModule,
    MatListModule,
    TextFieldModule,
    MatInputModule,
    MatFormFieldModule, 
    MatProgressSpinnerModule,

  ],
  exports: [
    MatButtonModule,
    MatListModule,
    TextFieldModule,
    MatInputModule,
    MatFormFieldModule,
    MatProgressSpinnerModule,
  ],
})
export class CustomMaterialModule { }
