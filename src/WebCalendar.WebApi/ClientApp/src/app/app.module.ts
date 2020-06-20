import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {ReactiveFormsModule} from '@angular/forms';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {HomeLayoutComponent} from './layout/home-layout/home-layout.component';
import {JwtInterceptor} from './core/interceptor/jwt.interceptor';
import {ErrorInterceptor} from './core/interceptor/error.interceptor';
import {MainLayoutComponent} from './layout/main-layout/main-layout.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {AngularFireModule} from '@angular/fire';
import {AngularFireMessagingModule} from '@angular/fire/messaging';
import {environment} from '../environments/environment';

@NgModule({
  declarations: [
    AppComponent,
    HomeLayoutComponent,
    MainLayoutComponent
  ],
  imports: [
    AngularFireModule.initializeApp(environment.firebaseConfig),
    AngularFireMessagingModule,
    NgbModule,
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},],
  bootstrap: [AppComponent]
})
export class AppModule {
}
