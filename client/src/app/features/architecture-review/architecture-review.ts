import { Component, inject, signal } from '@angular/core';
import { ArchitectureReviewService } from './architecture-review.service';
import { ArchitectureReviewResult } from './architecture-review.models';

@Component({
  selector: 'app-architecture-review',
  imports: [],
  templateUrl: './architecture-review.html',
  styleUrl: './architecture-review.scss',
})
export class ArchitectureReview {
  private readonly architectureReviewService = inject(ArchitectureReviewService);

  protected readonly description = signal(
    'Public S3 bucket serving user uploads directly, no WAF, no CloudFront, credentials hardcoded in the deployment script.',
  );
  protected readonly isAnalyzing = signal(false);
  protected readonly result = signal<ArchitectureReviewResult | null>(null);
  protected readonly errorMessage = signal<string | null>(null);

  protected onDescriptionInput(value: string): void {
    this.description.set(value);
  }

  protected runAnalysis(): void {
    this.isAnalyzing.set(true);
    this.errorMessage.set(null);
    this.result.set(null);

    this.architectureReviewService.analyze(this.description()).subscribe({
      next: (result) => {
        this.result.set(result);
        this.isAnalyzing.set(false);
      },
      error: () => {
        this.errorMessage.set('Could not reach the LuminoSec API. Is it running locally?');
        this.isAnalyzing.set(false);
      },
    });
  }
}
