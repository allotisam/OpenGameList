/**
 * @license
 * Copyright Google Inc. All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
import { AnimationEntryMetadata, Injector, ViewMetadata } from '@angular/core';
import { ComponentFixture, TestComponentBuilder } from '@angular/core/testing';
import { ConcreteType, Type } from '../src/facade/lang';
/**
 * A TestComponentBuilder that allows overriding based on the compiler.
 *
 * @deprecated Use `TestBed.configureTestModule` / `TestBed.override...` / `TestBed.createComponent`
 * instead.
*/
export declare class OverridingTestComponentBuilder extends TestComponentBuilder {
    constructor(injector: Injector);
    overrideTemplate(componentType: Type, template: string): OverridingTestComponentBuilder;
    overrideAnimations(componentType: Type, animations: AnimationEntryMetadata[]): TestComponentBuilder;
    overrideView(componentType: Type, view: ViewMetadata): OverridingTestComponentBuilder;
    overrideDirective(componentType: Type, from: Type, to: Type): OverridingTestComponentBuilder;
    overrideProviders(type: Type, providers: any[]): OverridingTestComponentBuilder;
    overrideViewProviders(type: Type, providers: any[]): OverridingTestComponentBuilder;
    createAsync<T>(rootComponentType: ConcreteType<T>): Promise<ComponentFixture<T>>;
    createSync<T>(rootComponentType: ConcreteType<T>): ComponentFixture<T>;
    private _applyMetadataOverrides();
}
