import { Component, OnInit, inject, signal } from '@angular/core';
import { ArchitectureReviewService } from './features/architecture-review/architecture-review.service';
import { ArchitectureReviewResult } from './features/architecture-review/architecture-review.models';
import { HealthService } from './core/health.service';

type ApiStatus = 'checking' | 'online' | 'offline';

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App implements OnInit {
  private readonly architectureReviewService = inject(ArchitectureReviewService);
  private readonly healthService = inject(HealthService);

  protected readonly apiStatus = signal<ApiStatus>('checking');
  protected readonly description = signal(
    'Public S3 bucket serving user uploads directly, no WAF, no CloudFront, credentials hardcoded in the deployment script.',
  );
  protected readonly isAnalyzing = signal(false);
  protected readonly result = signal<ArchitectureReviewResult | null>(null);
  protected readonly errorMessage = signal<string | null>(null);

  ngOnInit(): void {
    this.healthService.check().subscribe({
      next: () => this.apiStatus.set('online'),
      error: () => this.apiStatus.set('offline'),
    });
  }

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
